using System;
using Cysharp.Threading.Tasks;
using Project.Client.Src.com.AB.Infrastructure.InfrastructureAPI.ScreenTransition.SceneLoader;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace Project.Client.Src.com.AB.Infrastructure.Ui
{
    public class CommonUiController : IInitializable, IDisposable
    {
        public const string CAMERA_ID = "CommonUICamera";
        public const string CNVAS_ID = "CommonCanvas";

        Camera _uiCamera;
        Canvas _canvas;

        SignalBus _signals;

        public CommonUiController(Camera uiCamera, Canvas canvas, SignalBus signals)
        {
            _uiCamera = uiCamera;
            _canvas = canvas;

            _canvas.renderMode = RenderMode.ScreenSpaceCamera;
            _canvas.worldCamera = _uiCamera;
            _canvas.planeDistance = 1f;
            
            var rect = _canvas.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            
            _signals = signals;
        }

        public void Initialize()
        {
            _signals.Subscribe<SceneLoadSignal>(AttachUICameraToGameplayCamera);
            _signals.Subscribe<SceneLoaderSignal>(AttachUICameraToGameplayCamera);
            
            AttachUICameraToGameplayCamera();
        }

        public void Dispose()
        {
            _signals.Unsubscribe<SceneLoadSignal>(AttachUICameraToGameplayCamera);
            _signals.Unsubscribe<SceneLoaderSignal>(AttachUICameraToGameplayCamera);
        }

        public async void AttachUICameraToGameplayCamera()
        {
            await UniTask.DelayFrame(1);
            Camera baseCamera = Camera.main;

            if (baseCamera == null || _uiCamera == null)
            {
                Debug.LogWarning("UICameraManager: base or UI camera is null");
                return;
            }

            var baseData = baseCamera.GetUniversalAdditionalCameraData();
            var uiData = _uiCamera.GetUniversalAdditionalCameraData();

            uiData.renderType = CameraRenderType.Overlay;

            if (!baseData.cameraStack.Contains(_uiCamera))
                baseData.cameraStack.Add(_uiCamera);
        }

        [Serializable]
        public class Settings
        {
            public string ContainerID;
            public Canvas CommonUiCanvasPrefab;
            
            public Camera CommonUiCameraPrefab;
            public Vector3 UiCameraOffset;
        }
    }
}