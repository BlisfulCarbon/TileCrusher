using System.Linq;
using UnityEngine;
using Zenject;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Mined
{
    public class MinedInstaller : MonoInstaller
    {
        [SerializeField] DigGameSceneRefs _sceneRefs;
        [SerializeField] DigGameSoInstaller _settings;

        public override void InstallBindings()
        {
            BindPoolRef();

            Container.Bind<IMinedService>()
                .To<MinedService>()
                .AsSingle().NonLazy();
        }

        void BindPoolRef()
        {
            MinedPoolRef poolRef = new MinedPoolRef();
            foreach (var minedDef in _settings.Mined.Mined)
            {
                if(Container.HasBindingId<MinedMono.Pool>(minedDef.ID))
                    continue;
                
                Container.BindMemoryPool<MinedMono, MinedMono.Pool>()
                    .WithId(minedDef.ID)
                    .FromComponentInNewPrefab(minedDef.Prefab)
                    .UnderTransform(transform);

                poolRef.Items.Add(minedDef, Container.ResolveId<MinedMono.Pool>(minedDef.ID));
            }

            Container.BindInstance(poolRef);
        }
    }
}