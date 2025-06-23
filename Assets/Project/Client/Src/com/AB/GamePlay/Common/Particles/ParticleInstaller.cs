using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Project.Client.Src.com.AB.GamePlay.Common.Particles
{
    public class ParticleInstaller : MonoInstaller
    {
        [InlineEditor] [SerializeField] private ScriptableObject[] _mappers;
        IParticleMapper[] Mappers => _mappers.Select(item => (IParticleMapper)item).ToArray();
        
        const string PARTICLE_TRANSFORM_GROUP = "PartilcePool";

        public override void InstallBindings()
        {
            Container.BindInstance(transform).WithId(ParticlesConst.PARTICLES_CONTAINER_KEY);
            var goPoolContainer = CreatePoolContainer();
            
            BindParticlePools(Container, Mappers, goPoolContainer);

            Container.Bind<IParticleService>()
                .To<ParticleService>().AsSingle().NonLazy();
        }

        void BindParticlePools(
            DiContainer container, 
            IParticleMapper[] mappers, 
            GameObject goPoolContainer)
        {
            ParticlePoolRef poolRef = new();
            
            foreach (var mapper in mappers)
            foreach (var item in mapper.GetParticleMapping())
            {
                if(Container.HasBindingId<ParticleMono.Pool>(item.Key))
                    continue;
                
                container.BindMemoryPool<ParticleMono, ParticleMono.Pool>()
                    .WithId(item.Key)
                    .WithMaxSize(item.Def.MaxInstances)
                    .FromComponentInNewPrefab(item.Def.ParticlePrefab)
                    .UnderTransform(goPoolContainer.transform);

                var pool = Container.ResolveId<ParticleMono.Pool>(item.Key);
                poolRef.Items.Add(item.Key, (pool, item.Def));
            }

            Container.BindInstance(poolRef);
        }

        GameObject CreatePoolContainer()
        {
            GameObject goPoolContainer = new GameObject(PARTICLE_TRANSFORM_GROUP);
            goPoolContainer.transform.parent = this.transform;
            return goPoolContainer;
        }

        Dictionary<string, (ParticleMono.Pool, ParticleSo)> GetPools(IParticleMapper[] mappers)
        {
            var pools = new Dictionary<string, (ParticleMono.Pool, ParticleSo)>();

            foreach (var mapper in mappers)
            foreach (var item in mapper.GetParticleMapping())
                pools[item.Key] = (Container.ResolveId<ParticleMono.Pool>(item.Key), item.Def);
            return pools;
        }
    }
}