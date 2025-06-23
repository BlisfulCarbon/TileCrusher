using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.Common.Particles
{
    public interface IParticleService
    {
        public void Spawn(string key, Vector3 position);
    }
}