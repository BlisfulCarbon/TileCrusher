using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Project.Client.Src.com.AB.GamePlay.Common.Dev
{
    public class InitLevel : MonoBehaviour
    {
        public Tilemap DrawingLayer;

        public GameObject TilePrefab;

        public void Awake()
        {
            List<Vector3> drawingPositions = GetAllPositionLayer(DrawingLayer);
            DrawingLayer.gameObject.SetActive(false);

            foreach (var position in drawingPositions)
            {
                CreateTile(position, "1");
                CreateTile(position, "2", -0.1f);
                CreateTile(position, "3", -0.2f);
                CreateTile(position, "4", -0.3f);
            }
        }

        void CreateTile(Vector3 position, string syfixName, float yOffset = 0f)
        {
            var tile = GameObject.Instantiate(TilePrefab);
            tile.transform.position = new Vector3(position.x, position.y + yOffset, position.z);
            tile.name = $"Tile.{syfixName}.{position}";
        }


        public List<Vector3> GetAllPositionLayer(Tilemap layer)
        {
            List<Vector3> layerPositions = new List<Vector3>();

            foreach (var pos in layer.cellBounds.allPositionsWithin)
            {
                Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                Vector3 place = layer.CellToWorld(localPlace);
                if (layer.HasTile(localPlace))
                {
                    layerPositions.Add(place);
                }
            }

            return layerPositions;
        }
    }
}