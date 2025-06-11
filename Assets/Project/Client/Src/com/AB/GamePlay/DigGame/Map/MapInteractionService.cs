using System;
using Plugins.PaperCrafts.com.AB.Extensions;
using Project.Client.Src.com.AB.GamePlay.Common.Particles;
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
        readonly Camera _gamePlayCamera;

        readonly CompositeDisposable _disposables = new();
        float _SwipeHoldInteractionDelay;

        public MapInteractionService(
            Settings settings,
            MapGamePlayService mapGamePlay,
            ParticleService particles,
            IInputService input,
            [Inject(Id = ContainersID.GAMEPLAY_CAMERA_CONTAINER_ID)]
            Camera gamePlayCamera)
        {
            _settings = settings;
            _input = input;
            _mapGamePlay = mapGamePlay;
            _particles = particles;
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

                if (layer.Tilemap.HasTile(cellPosition))
                {
                    Vector2Int cellPositionV2 = cellPosition.ToVector2Int();

                    int countInteractionsWithCell = 0;
                    layer.InteractionsMap.TryGetValue(cellPositionV2, out countInteractionsWithCell);

                    countInteractionsWithCell++;
                    layer.InteractionsMap[cellPositionV2] = countInteractionsWithCell;

                    TileBase tile = null;
                    if (countInteractionsWithCell < layer.Def.TopologyTiles.Count)
                        tile = layer.Def.TopologyTiles[countInteractionsWithCell];

                    layer.Tilemap.SetTile(cellPosition, tile);

                    
                    var particleSpawnPosition = layer.Tilemap.CellToWorld(cellPosition);
                    var particleKey = tile == null ? layer.Def.GetBrokenKey() : layer.Def.GetBreakKey();
                    _particles.Spawn(particleKey, particleSpawnPosition);

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