using Plugins.PaperCrafts.com.AB.Extensions;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Project.Client.Src.com.AB.GamePlay.Common.Dev
{
    public class TileBreaker : MonoBehaviour
    {
        public float OffsetY;
        public Camera Camera;

        public Tilemap FirstLayer;
        public Tilemap SecondLayer;
        public Tilemap ThirdLayer;
        public Tilemap FourthLayer;

        void Start()
        {
            foreach (var position in FirstLayer.cellBounds.allPositionsWithin)
            {
                if (FirstLayer.HasTile(position))
                {
                    Debug.Log($"Has tile {position}");
                }
            }
        }


        // void Update()
        // {
        //     if (Input.anyKeyDown)
        //     {
        //         Vector3 inputWorldPoint = Camera.ScreenToWorldPoint(Input.mousePosition);
        //         DeleteTopTile(inputWorldPoint);
        //     }
        // }

        void DeleteTopTile(Vector3 inputWorldPoint)
        {
            Debug.Log($"Delete top {inputWorldPoint}");
        
            bool tileWasRemove = false;
            tileWasRemove = IsExistTileRemove(FirstLayer, inputWorldPoint);
            if (tileWasRemove)
                return;

            tileWasRemove = IsExistTileRemove(SecondLayer, inputWorldPoint);
            if (tileWasRemove)
                return;

            tileWasRemove = IsExistTileRemove(ThirdLayer, inputWorldPoint);
            if (tileWasRemove)
                return;
        
            tileWasRemove = IsExistTileRemove(FourthLayer, inputWorldPoint);
            if (tileWasRemove)
                return;
        
        }

        bool IsExistTileRemove(Tilemap layer, Vector3 worldPosition)
        {
            Vector3Int cellPosition = layer.WorldToCell(worldPosition.OffsetY(worldPosition.y).DefaultZ());
            
            Debug.Log($"World point: {worldPosition}, Find cell: {cellPosition}");
        
            cellPosition = new Vector3Int(cellPosition.x, cellPosition.y, 0);
            bool hastTile = layer.HasTile(cellPosition);
            if (hastTile)
            {
                Debug.Log($"Delete::{layer.name}::{cellPosition}");
                layer.SetTile(cellPosition, null);
            }
        
            return hastTile;
        }
    }
}