using Plugins.PaperCrafts.com.AB.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Project.Client.Src.com.AB.GamePlay.Common.Particles
{
    public class ParticleMono : MonoBehaviour
    {
        [SerializeField] ParticleSystem _particleInstance;

        public void Active(bool active) => 
            _particleInstance.gameObject.SetActive(active);

        [Button]
        public void Play()
        {
            Active(true);
            _particleInstance.Play();
        }
        
        [Button]
        public void Stop()
        {
            _particleInstance.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            Active(false);
        }

        public class Pool : MemoryPool<Vector3, ParticleMono>
        {
            protected override void Reinitialize(Vector3 position, ParticleMono item) => 
                item.SetPosition(position);
    
            protected override void OnSpawned(ParticleMono item) => 
                item.Play();

            protected override void OnDespawned(ParticleMono item) => 
                item.Stop();
        }
    }
}