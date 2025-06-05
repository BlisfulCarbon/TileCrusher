using UnityEngine;
using UnityEngine.Tilemaps;

namespace Project.Client.Src.com.AB.GamePlay.Common.Dev
{
    public class CustomTileMapRenderer : MonoBehaviour
    {
        public Tilemap tilemap;
        public int zOffset;

        void CustomSort()
        {
            foreach (var position in tilemap.cellBounds.allPositionsWithin)
            {
                if (tilemap.HasTile(position))
                {
                    Vector3 worldPos = tilemap.CellToWorld(position);
                    int newZ =
                        CalculateCustomSortingOrder(position, tilemap.cellBounds.min, tilemap.cellBounds.max);

                    var tile = tilemap.GetTile(position);
                    tilemap.SetTile(position, null); 
                    tilemap.SetTile(new Vector3Int(position.x, position.y, newZ), tile);
                }
            }
        }

        int CalculateCustomSortingOrder(Vector3 position, Vector3Int min, Vector3Int max)
        {
            int z = zOffset - (min.x - (int)position.x)*2;

            if (position.x % 2 > 0)
                z += 1;
            
            Debug.Log($"{position}...{min}...{max}...{z}");
            return z;
        }
    }
}