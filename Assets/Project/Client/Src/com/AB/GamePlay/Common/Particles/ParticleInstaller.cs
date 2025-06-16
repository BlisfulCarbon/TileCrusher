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

            foreach (var mapper in Mappers)
            foreach (var item in mapper.GetParticleMapping())
            {
                Container.BindMemoryPool<ParticleMono, ParticleMono.Pool>()
                    .WithId(item.Key)
                    .WithMaxSize(item.Def.MaxInstances)
                    .FromComponentInNewPrefab(item.Def.ParticlePrefab)
                    .UnderTransform(goPoolContainer.transform);
            }

            Container.Bind<ParticleService>().FromMethod(ctx =>
            {
                var pools = new Dictionary<string, (ParticleMono.Pool, ParticleSo)>();

                foreach (var mapper in Mappers)
                foreach (var item in mapper.GetParticleMapping())
                    pools[item.Key] = (Container.ResolveId<ParticleMono.Pool>(item.Key), item.Def);

                return new ParticleService(pools);
            }).AsSingle().NonLazy();
        }

        GameObject CreatePoolContainer()
        {
            GameObject goPoolContainer = new GameObject(PARTICLE_TRANSFORM_GROUP);
            goPoolContainer.transform.parent = this.transform;
            return goPoolContainer;
        }
    }
}