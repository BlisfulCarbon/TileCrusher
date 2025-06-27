using System;
using System.Collections.Generic;
using System.Linq;
using Project.Client.Src.com.AB.GamePlay.DigGame.Map;
using Project.Client.Src.com.AB.GamePlay.DigGame.Mined;
using Project.Client.Src.com.AB.GamePlay.DigGame.React;
using Project.Client.Src.com.AB.Infrastructure.InfrastructureAPI.Input;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Logic
{
    public class DigGameLogicService : IDisposable
    {
        readonly Settings _settings;
        readonly IInputService _input;
        readonly IMapService _map;
        readonly IMinedService _mined;
        readonly IReactService _react;
        readonly Camera _gamePlayCamera;

        readonly CompositeDisposable _disposables = new();

        float _SwipeHoldInteractionDelay;
        readonly DigGameLogicInteractionHandler _interactChain;

        public DigGameLogicService(
            Settings settings,
            IMapService map,
            IInputService input,
            IMinedService mined,
            IReactService react,
            [Inject(Id = ContainersID.GAMEPLAY_CAMERA_CONTAINER_ID)]
            Camera gamePlayCamera, List<DigGameLogicInteractionHandler> interactionFlow)
        {
            _settings = settings;
            _input = input;
            _map = map;
            _mined = mined;
            _react = react;
            _gamePlayCamera = gamePlayCamera;

            _input.OnTap.Subscribe(OnTap).AddTo(_disposables);
            _input.OnHold.Subscribe(OnHold).AddTo(_disposables);
            _input.OnSwipe.Subscribe(OnSwipe).AddTo(_disposables);

            _interactChain = ToChainOfInteractFlow(interactionFlow);
        }

        DigGameLogicInteractionHandler ToChainOfInteractFlow(List<DigGameLogicInteractionHandler> interactionFlow)
        {
            if (interactionFlow.Count == 0)
            {
                Debug.LogWarning($"{nameof(DigGameLogicService)}::BindInteractionFlow: interactionFlow is empty.");
                return null;
            }

            for (var i = 0; i < interactionFlow.Count - 1; i++) 
                interactionFlow[i].SetNext(interactionFlow[i + 1]);

            return interactionFlow.First();
        }

        void ProcessInteractions(Vector2 worldPosition)
        {
            _interactChain.Handle(worldPosition);
        }


        void OnTap(Vector2 position)
        {
            var worldPosition = ScreenToWorld(position);
            ProcessInteractions(worldPosition);
        }

        void OnHold(Vector2 position)
        {
            Debug.Log($"MapInteractionService::OnHold");
        }

        void OnSwipe(Vector2 position)
        {
            Debug.Log($"MapInteractionService::OnSwipe");
        }

        DigGameLogicInteractionHandler BuildInteractionChain()
        {
            var session = new DigGameLogicInteractionHandler.LogicSession();

            var resetSession = new LogicProvideSessionHandler(session);
            var getTopTile = new LogicMapGetTopCellPositionHandler(session, _map);
            var breakMined = new LogicMinedBreakHandler(session, _mined);
            var breakTile = new LogicMapTileBreakHandler(session, _map);
            var produceReaction = new LogicProduceReaction(session, _react);
            var spawnMined = new LogicMinedSpawnHandler(session, _mined);

            resetSession.SetNext(getTopTile);
            getTopTile.SetNext(breakMined);
            breakMined.SetNext(breakTile);
            breakTile.SetNext(produceReaction);
            produceReaction.SetNext(spawnMined);

            return resetSession;
        }


        public void DigTile(Vector2 worldPosition)
        {
            /*

            foreach (var layer in _mapGamePlay.Layers)
            {
                Vector3Int cellPosition = layer.Tilemap.WorldToCell(worldPosition);
                Vector2Int cellPositionV2 = cellPosition.ToVector2Int();

                if (layer.IsMinedAttach(cellPositionV2, out MapTileState tileSate))
                {
                    const int SHAKE_COUNT_MAX = 3;
                    MinedMono attachedMined = tileSate.AttachedMined;
                    attachedMined.Break();
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
            */
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