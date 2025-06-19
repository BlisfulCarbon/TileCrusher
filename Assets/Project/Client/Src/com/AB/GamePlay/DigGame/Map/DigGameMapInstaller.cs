using Project.Client.Src.com.AB.GamePlay.DigGame.Map.Filling;
using UnityEngine;
using Zenject;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Map
{
    public class DigGameMapInstaller : MonoInstaller
    {
        [SerializeField] DigGameSceneRefs _sceneRefs;
        [SerializeField] DigGameSoInstaller _settings;

        public override void InstallBindings()
        {
            Container.Bind<MapGamePlayService>().AsSingle().NonLazy();

            Container.BindInstance(_sceneRefs.GamePlayLayersContainer)
                .WithId(ContainersID.GAME_PALY_LAYERS_CONTAINER_ID);

            Container.BindInterfacesTo<MapFillingService>()
                .AsSingle().NonLazy();

            Container.BindInstance(_sceneRefs.FillingLayer)
                .WithId(ContainersID.FILLING_LAYER_CONTAINER_ID);

            Container.BindInstance(_sceneRefs.Camera)
                .WithId(ContainersID.GAMEPLAY_CAMERA_CONTAINER_ID);
        }
    }
}