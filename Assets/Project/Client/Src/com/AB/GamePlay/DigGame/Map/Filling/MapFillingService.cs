using UnityEngine.Tilemaps;
using Zenject;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Map.Filling
{
    public class MapFillingService : IInitializable
    {
        MapFacade _map;
        Tilemap _fillingLayer;

        public MapFillingService(
            [Inject(Id = Settings.FILLING_LAYER_CONTAINER_ID)]
            Tilemap fillingLayer,
            MapFacade map
        )
        {
            _fillingLayer = fillingLayer;
            _map = map;

            _fillingLayer.gameObject.SetActive(false);
        }

        public void Initialize()
        {
            FillLayers(_map, _fillingLayer);
        }

        void FillLayers(MapFacade map, Tilemap fillingLayer)
        {
            foreach (var fillingPosition in fillingLayer.cellBounds.allPositionsWithin)
            {
                if (!fillingLayer.HasTile(fillingPosition))
                    continue;

                map.SetTileAllLayers(fillingPosition);
            }
        }

        public class Settings
        {
            public const string FILLING_LAYER_CONTAINER_ID = "FillingLayer";
        }
    }
}