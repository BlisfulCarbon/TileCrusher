using System;
using System.Collections.Generic;
using Project.Client.Src.com.AB.GamePlay.DigGame.Map.Filling;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Map
{
    public class MapGamePlayLayersService
    {
        Settings _settings;
        MapFacade _registry;
        
        public MapGamePlayLayersService(Settings settings, MapFacade registry,
            [Inject(Id = Settings.GAME_PALY_LAYERS_CONTAINER_ID)] Transform gamePlayLayersContainer)
        {
            _settings = settings;
            _registry = registry;

            _registry.AddNewLayers(
                _settings.LayerPrefab, 
                _settings.GamePlayLayers, 
                gamePlayLayersContainer, 
                _settings.Offset);
            
        }

        [Serializable]
        public class Settings
        {
            public const string GAME_PALY_LAYERS_CONTAINER_ID = "GamePlayContainer";

            public Vector3 Offset;
            public Tilemap LayerPrefab;
            public List<MapFillingLayerSo> GamePlayLayers;
        }
    }
}