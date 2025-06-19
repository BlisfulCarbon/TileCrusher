using System;
using System.Collections.Generic;
using System.Linq;
using Project.Client.Src.com.AB.GamePlay.Common.Audio;
using Project.Client.Src.com.AB.GamePlay.Common.Particles;
using Project.Client.Src.com.AB.GamePlay.DigGame.Map.Filling;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Map
{
    public class MapGamePlayService
    {
        Settings _settings;
        readonly Dictionary<string, MapLayerFacade> _layers = new Dictionary<string, MapLayerFacade>();

        public IEnumerable<MapLayerFacade> Layers
            => _layers.Values;

        public MapGamePlayService(Settings settings,
            [Inject(Id = ContainersID.GAME_PALY_LAYERS_CONTAINER_ID)]
            Transform gamePlayLayersContainer)
        {
            _settings = settings;

            AddLayers(_settings, gamePlayLayersContainer);
        }

        void AddLayers(Settings settings, Transform container)
        {
            foreach (var layerElement in settings.GamePlayLayers.Select((item, index) => (index, item)))
            {
                MapLayerFacade mapLayer = new MapLayerFacade(
                    layerElement.item,
                    settings.LayerPrefab, container,
                    layerElement.index * settings.NextLayerOffset);

                mapLayer.SetOrder(-_layers.Count + settings.LayerOriginOrdering);
                _layers.Add(mapLayer.GetId, mapLayer);
            }
        }

        public void SetTileAllLayers(Vector3Int position, int topologyId = 0)
        {
            foreach (MapLayerFacade layer in _layers.Values)
                layer.SetTile(position, topologyId);
        }

        [Serializable]
        public class Settings : IParticleMapper, IAudioMapper
        {
            public Tilemap LayerPrefab;
            public Vector3 NextLayerOffset;
            public int LayerOriginOrdering;

            public List<MapFillingLayerSo> GamePlayLayers;

            public IEnumerable<ParticleMappingDto> GetParticleMapping()
            {
                List<ParticleMappingDto> particlesDef = new();
                GamePlayLayers.ForEach(item => particlesDef.AddRange(item.GetParticleMapping()));

                return particlesDef;
            }

            public IEnumerable<AudioMappingDto> GetAudiosMapping()
            {
                List<AudioMappingDto> audiosDef = new();
                GamePlayLayers.ForEach(item => audiosDef.AddRange(item.GetAudioMapping()));

                return audiosDef;
            }
        }
    }
}