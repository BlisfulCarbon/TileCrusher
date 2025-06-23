using UnityEngine.Tilemaps;
using Zenject;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Map.Filling
{
    public class MapFillingService : IInitializable
    {
        IMapService _map;
        Tilemap _fillingLayer;

        public MapFillingService(
            [Inject(Id = ContainersID.FILLING_LAYER_CONTAINER_ID)]
            Tilemap fillingLayer,
            IMapService map
        )
        {
            _fillingLayer = fillingLayer;
            _map = map;

            _fillingLayer.gameObject.SetActive(false);
        }

        public void Initialize() => 
            FillLayers(_map, _fillingLayer);

        void FillLayers(IMapService map, Tilemap fillingLayer)
        {
            foreach (var fillingPosition in fillingLayer.cellBounds.allPositionsWithin)
            {
                if (!fillingLayer.HasTile(fillingPosition))
                    continue;

                map.SetTileAllLayers(fillingPosition);
            }
        }
    }
}