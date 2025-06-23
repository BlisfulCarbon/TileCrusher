using System;
using System.Collections.Generic;
using System.Linq;
using Plugins.PaperCrafts.com.AB.Extensions;
using Project.Client.Src.com.AB.GamePlay.Common.Audio;
using Project.Client.Src.com.AB.GamePlay.Common.Particles;
using Project.Client.Src.com.AB.GamePlay.DigGame.Map.Filling;
using Project.Client.Src.com.AB.GamePlay.DigGame.React;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Map
{
    public class MapService : IMapService
    {
        readonly Settings _settings;

        readonly MapLayerFacade[] _layers;
        readonly AudioSFXService _audio;
        readonly IReactService _react;
        readonly ParticleService _particles;

        public MapService(Settings settings,
            [Inject(Id = ContainersID.GAME_PALY_LAYERS_CONTAINER_ID)]
            Transform gamePlayLayersContainer, IReactService react)
        {
            _settings = settings;
            _react = react;

            _layers = new MapLayerFacade[_settings.GamePlayLayers.Count];
            AddLayers(_settings, gamePlayLayersContainer);
        }

        public MapCell GetTopCell(Vector2 worldPosition)
        {
            for (int i = 0; i < _layers.Length; i++)
            {
                MapLayerFacade layer = _layers[i];

                Tilemap tilemap = layer.GetTilemap();
                Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
                if (tilemap.HasTile(cellPosition))
                    return new(cellPosition.DefaultZ(i), tilemap.CellToWorld(cellPosition));
            }

            return GetLastCell(worldPosition);
        }

        public (bool wasBreak, bool wasBroken) Break(Vector3Int cellPosition, out IReact react)
        {
            react = null;
            
            if (!HasLayer(cellPosition)) return (false, false);
            MapLayerFacade layer = GetLayer(cellPosition);
            bool hastTile = layer.HasTile(cellPosition.DefaultZ());
            if (!hastTile) return (false, false);

            layer.Break(cellPosition.ToVector2Int(), out bool broken);

            react = _react.GetReact(layer.GetActions(), broken);
            return (true, broken);
        }

        MapCell GetLastCell(Vector2 worldPosition)
        {
            Tilemap tilemap = _layers.Last().GetTilemap();
            Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);

            return new(cellPosition.DefaultZ(_layers.Length), tilemap.CellToWorld(cellPosition));
        }

        bool HasLayer(Vector3Int cellPosition) =>
            cellPosition.z < _layers.Length && cellPosition.z >= 0;

        MapLayerFacade GetLayer(Vector3Int cellPosition) =>
            _layers[cellPosition.z];

        void AddLayers(Settings settings, Transform container)
        {
            foreach (var layerDef in settings.GamePlayLayers.Select((item, index) => (item, index)))
            {
                MapLayerFacade mapLayer = new MapLayerFacade(
                    layerDef.item.LayerID,
                    layerDef.item,
                    settings.LayerPrefab, container,
                    layerDef.index * settings.NextLayerOffset);

                mapLayer.SetOrder(settings.LayerOriginOrdering - layerDef.index);
                _layers[layerDef.index] = mapLayer;
            }
        }

        public void SetTileAllLayers(Vector3Int position, int topologyId = 0)
        {
            foreach (MapLayerFacade layer in _layers)
                layer.SetTile(position, topologyId);
        }

        [Serializable]
        public class Settings : IParticleMapper, IAudioMapper
        {
            public Tilemap LayerPrefab;
            public Vector3 NextLayerOffset;
            public int LayerOriginOrdering;

            public List<MapFillingLayerSo> GamePlayLayers;

            public IEnumerable<ParticleDto> GetParticleMapping()
            {
                List<ParticleDto> particlesDef = new();
                GamePlayLayers.ForEach(item => 
                    particlesDef.AddRange(item.Actions.GetParticleMapping()));

                return particlesDef;
            }

            public IEnumerable<AudioDto> GetAudiosMapping()
            {
                List<AudioDto> audiosDef = new();
                GamePlayLayers.ForEach(item => 
                    audiosDef.AddRange(item.Actions.GetAudioMapping()));

                return audiosDef;
            }
        }
    }
}