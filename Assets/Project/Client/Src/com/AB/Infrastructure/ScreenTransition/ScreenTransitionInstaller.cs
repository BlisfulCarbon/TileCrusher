using System;
using Project.Client.Src.com.AB.Infrastructure.InfrastructureAPI.ScreenTransition.Fader;
using Project.Client.Src.com.AB.Infrastructure.InfrastructureAPI.ScreenTransition.SceneLoader;
using Project.Client.Src.com.AB.Infrastructure.Ui;
using Project.Client.Src.com.AB.Infrastructure.ScreenTransition.Fader;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Project.Client.Src.com.AB.Infrastructure.ScreenTransition
{
    public class ScreenTransitionInstaller : Installer
    {
        readonly InfrastructureInstallerSo _def;

        public ScreenTransitionInstaller(InfrastructureInstallerSo def) =>
            _def = def;

        public override void InstallBindings()
        {
            GameObject uiContainer = InitCommonUi();
            InitFader();
            InitSceneLoader();
        }

        GameObject InitCommonUi()
        {
            var commonContainer = new GameObject();
            Object.DontDestroyOnLoad(commonContainer.gameObject);
            commonContainer.gameObject.name = _def.CommonUi.ContainerID;
            Container.BindInstance(commonContainer)
                .WithId(_def.CommonUi.ContainerID);

            var canvas = Container.InstantiatePrefabForComponent<Canvas>(_def.CommonUi.CommonUiCanvasPrefab);
            canvas.transform.SetParent(commonContainer.transform);

            var uiCamera = Container.InstantiatePrefabForComponent<Camera>(_def.CommonUi.CommonUiCameraPrefab);
            Container.BindInstance(uiCamera).WithId(CommonUiController.CAMERA_ID);

            uiCamera.transform.SetParent(commonContainer.transform);
            uiCamera.transform.position += _def.CommonUi.UiCameraOffset;

            Container.Bind<CommonUiController>().AsSingle()
                .WithArguments(canvas, uiCamera);
            Container.Bind<IInitializable>()
                .To<CommonUiController>().FromResolve();
            Container.Bind<IDisposable>()
                .To<CommonUiController>().FromResolve();

            return commonContainer;
        }

        void InitFader()
        {
            var faderInstance = Container.InstantiatePrefabForComponent<FaderMono>(_def.Fader.FaderPrefab);

            Container.Bind<FaderMono>().FromInstance(faderInstance).AsSingle();

            Container.DeclareSignal<FaderInSignal>();
            Container.DeclareSignal<FaderOutSignal>();

            Container.BindInterfacesAndSelfTo<FadeController>().AsSingle().NonLazy();
        }

        void InitSceneLoader()
        {
            Container.DeclareSignal<SceneLoadSignal>();
            Container.DeclareSignal<SceneLoaderSignal>();

            Container.BindInterfacesAndSelfTo<SceneLoaderController>().AsSingle().NonLazy();

            Container.Bind<ScenePayloadService>().AsSingle();
        }
    }
}