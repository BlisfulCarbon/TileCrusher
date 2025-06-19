using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Mined
{
    public interface IMinedService
    {
        public bool TrySpawn(Vector2 position, out MinedMono mined);
        
    }
}