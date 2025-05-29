using System;
using Cysharp.Threading.Tasks;
using Plugins.PaperCrafts.com.AB.Editor;
using Project.Client.Src.com.AB.Infrastructure.InfrastructureAPI.ScreenTransition.SceneLoader;
using Project.Client.Src.com.AB.Infrastructure.ScreenTransition.Fader;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Project.Client.Src.com.AB.Infrastructure.ScreenTransition
{
    public class SceneLoaderController : IInitializable, IDisposable
    {
        const float SCENE_LOADED = 0.9f;

        readonly Settings _settings;
        readonly SignalBus _signals;
        readonly ScenePayloadService _payloadService;
        readonly FadeController _fader;

        public SceneLoaderController(Settings settings, FadeController fader, SignalBus signals,
            ScenePayloadService payloadService)
        {
            _fader = fader;
            _settings = settings;
            _signals = signals;
            _payloadService = payloadService;
        }

        public void Initialize() => 
            _signals.Subscribe<SceneLoadSignal>(OnRequestSceneLoad);

        public void Dispose() => 
            _signals.Unsubscribe<SceneLoadSignal>(OnRequestSceneLoad);

        public async void OnRequestSceneLoad(SceneLoadSignal signal)
        {
            if (signal.Payload != null)
                _payloadService.Store(signal.Payload);

            await _fader.WaitUntilFadeIn(new());
            await UniTask.Yield();

            await SceneManager.LoadSceneAsync(_settings.LoadingScene, LoadSceneMode.Single);
            await UniTask.Yield();

            AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(signal.SceneName, LoadSceneMode.Additive);
            sceneLoading.allowSceneActivation = false;

            await _fader.WaitUntilFadeOut(new());
            var delayTask = UniTask.Delay(TimeSpan.FromSeconds(_settings.MinimumSceneLoadingActive));

            while (!signal.AllowSceneActivation || sceneLoading.progress < SCENE_LOADED) await UniTask.Yield();
            await delayTask;

            await _fader.WaitUntilFadeIn(new());

            sceneLoading.allowSceneActivation = true;
            await UniTask.WaitUntil(() => sceneLoading.isDone);
            await SceneManager.UnloadSceneAsync(_settings.LoadingScene);

            await UniTask.Yield();
            await _fader.WaitUntilFadeOut(new());

            _signals.Fire(new SceneLoaderSignal(signal.SceneName));
        }


        [Serializable]
        public class Settings
        {
            public SceneReference LoadingScene;
            public float MinimumSceneLoadingActive = 2f;
        }
    }
}