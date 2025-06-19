using System;
using Plugins.PaperCrafts.com.AB.Extensions;
using Project.Client.Src.com.AB.GamePlay.Common.Audio;
using Project.Client.Src.com.AB.GamePlay.Common.Particles;
using Project.Client.Src.com.AB.GamePlay.DigGame.Mined;
using Project.Client.Src.com.AB.Infrastructure.InfrastructureAPI.Input;
using UniRx;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Map
{
    public class MapInteractionService : IDisposable
    {
        readonly Settings _settings;
        readonly IInputService _input;
        readonly MapGamePlayService _mapGamePlay;
        readonly ParticleService _particles;
        readonly AudioSFXService _audioSfx;
        readonly IMinedService _mined;
        readonly Camera _gamePlayCamera;

        readonly CompositeDisposable _disposables = new();
        float _SwipeHoldInteractionDelay;

        public MapInteractionService(
            Settings settings,
            MapGamePlayService mapGamePlay,
            ParticleService particles,
            AudioSFXService audioSfx,
            IInputService input,
            IMinedService mined,
            [Inject(Id = ContainersID.GAMEPLAY_CAMERA_CONTAINER_ID)]
            Camera gamePlayCamera)
        {
            _settings = settings;
            _input = input;
            _mapGamePlay = mapGamePlay;
            _particles = particles;
            _audioSfx = audioSfx;
            _mined = mined;
            _gamePlayCamera = gamePlayCamera;

            _input.OnTap.Subscribe(OnTap).AddTo(_disposables);
            _input.OnHold.Subscribe(OnHold).AddTo(_disposables);
            _input.OnSwipe.Subscribe(OnSwipe).AddTo(_disposables);
        }

        void OnTap(Vector2 position)
        {
            var worldPosition = ScreenToWorld(position);
            DigTile(worldPosition);
        }

        void OnHold(Vector2 position)
        {
            Debug.Log($"MapInteractionService::OnHold");
        }

        void OnSwipe(Vector2 position)
        {
            Debug.Log($"MapInteractionService::OnSwipe");
        }

        public void DigTile(Vector2 worldPosition)
        {
            foreach (var layer in _mapGamePlay.Layers)
            {
                Vector3Int cellPosition = layer.Tilemap.WorldToCell(worldPosition);
                Vector2Int cellPositionV2 = cellPosition.ToVector2Int();

                if (layer.IsMinedAttach(cellPositionV2, out MapTileState tileSate))
                {
                    const int SHAKE_COUNT_MAX = 3;
                    MinedMono attachedMined = tileSate.AttachedMined;
                    attachedMined.Hit();
                    bool isOverHit = attachedMined.HitCount >= SHAKE_COUNT_MAX;
                    
                    if (isOverHit)
                    {
                        tileSate.AttachedMined = null;
                        _mined.PlayCollected(attachedMined.)
                        GameObject.Destroy(attachedMined);
                    }
                }

                if (layer.Tilemap.HasTile(cellPosition))
                {
                    int countInteractionsWithCell = 0;
                    layer.InteractionsMap.TryGetValue(cellPositionV2, out countInteractionsWithCell);

                    countInteractionsWithCell++;
                    layer.InteractionsMap[cellPositionV2] = countInteractionsWithCell;

                    TileBase tile = null;
                    if (countInteractionsWithCell < layer.Def.TopologyTiles.Count)
                        tile = layer.Def.TopologyTiles[countInteractionsWithCell];

                    layer.Tilemap.SetTile(cellPosition, tile);

                    var worldCellPosition = layer.Tilemap.CellToWorld(cellPosition);
                    var particleKey = tile == null ? layer.Def.GetParticleBrokenKey() : layer.Def.GetParticleBreakKey();
                    _particles.Spawn(particleKey, worldCellPosition);

                    var soundKey = tile == null ? layer.Def.GetAudioBrokenKey() : layer.Def.GetAudioBreakKey();
                    _audioSfx.Play(soundKey);

                    if (tile == null)
                    {
                        if (_mined.TrySpawn(worldCellPosition, out MinedMono mined))
                            layer.AddMined(cellPositionV2, mined);
                    }

                    break;
                }
            }
        }

        Vector2 ScreenToWorld(Vector2 position) =>
            _gamePlayCamera.ScreenToWorldPoint(position);

        public void Dispose() =>
            _disposables.Dispose();

        [Serializable]
        public class Settings
        {
            public float InteractionSwipeHoldDelay;
        }
    }
}