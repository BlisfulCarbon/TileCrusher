using UnityEngine.Tilemaps;
using Zenject;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Map.Filling
{
    public class MapFillingService : IInitializable
    {
        MapGamePlayService _mapGamePlay;
        Tilemap _fillingLayer;

        public MapFillingService(
            [Inject(Id = ContainersID.FILLING_LAYER_CONTAINER_ID)]
            Tilemap fillingLayer,
            MapGamePlayService mapGamePlay
        )
        {
            _fillingLayer = fillingLayer;
            _mapGamePlay = mapGamePlay;

            _fillingLayer.gameObject.SetActive(false);
        }

        public void Initialize() => 
            FillLayers(_mapGamePlay, _fillingLayer);

        void FillLayers(MapGamePlayService mapGamePlay, Tilemap fillingLayer)
        {
            foreach (var fillingPosition in fillingLayer.cellBounds.allPositionsWithin)
            {
                if (!fillingLayer.HasTile(fillingPosition))
                    continue;

                mapGamePlay.SetTileAllLayers(fillingPosition);
            }
        }
    }
}