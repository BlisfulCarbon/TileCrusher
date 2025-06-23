using Project.Client.Src.com.AB.GamePlay.DigGame.React;
using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Mined
{
    public interface IMinedService
    {
        public bool Break(Vector3Int cellPosition, out IReact react);
        
        public bool TrySpawn(Vector3Int cellPosition, Vector2 position);
    }
}