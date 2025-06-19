using System.Linq;
using UnityEngine;
using Zenject;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Mined
{
    public class DigGameMinedInstaller : MonoInstaller
    {
        [SerializeField] DigGameSceneRefs _sceneRefs;
        [SerializeField] DigGameSoInstaller _settings;

        public override void InstallBindings()
        {
        
            foreach (var minedDef in _settings.Mined.Mined)
            {
                Container.BindMemoryPool<MinedMono, MinedMono.Pool>()
                    .WithId(minedDef.ID)
                    .FromComponentInNewPrefab(minedDef.Prefab)
                    .UnderTransform(transform);
            }
            
            
            Container.Bind<IMinedService>().FromMethod(() => 
                    new MinedService(
                        _settings.Mined, 
                        _settings.Mined.Mined
                            .ToDictionary(
                                item=> item, 
                                item => Container.ResolveId<MinedMono.Pool>(item.ID))))
                .AsSingle().NonLazy();   

        }
    }
}