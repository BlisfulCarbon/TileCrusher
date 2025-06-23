using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Map
{
    public class MapCell
    {
        public Vector3Int GridPosition;
        public Vector2 WorldPosition;

        public MapCell()
        {
            GridPosition = default;
            WorldPosition = default;
        }
        
        public MapCell(Vector3Int gridPosition, Vector3 worldPosition)
        {
            GridPosition = gridPosition;
            WorldPosition = worldPosition;
        }
    }
}