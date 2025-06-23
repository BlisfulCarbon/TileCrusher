using Project.Client.Src.com.AB.GamePlay.DigGame.React;
using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Map
{
    public interface IMapService
    {
        public (bool wasBreak, bool wasBroken) Break(Vector3Int cellPosition, out IReact react);
        public MapCell GetTopCell(Vector2 worldPosition);

        public void SetTileAllLayers(Vector3Int position, int topologyId = 0);
    }
}