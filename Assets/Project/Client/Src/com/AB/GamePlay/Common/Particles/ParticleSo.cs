using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.Common.Particles
{
    [CreateAssetMenu(menuName = "Particles/ParticleSO")]
    public class ParticleSo : ScriptableObject
    {
        public ParticleMono ParticlePrefab;
        public float LifeTime;
        public int MaxInstances = 10;
    }
}