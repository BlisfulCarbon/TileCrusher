using System;
using System.Linq;
using UnityEngine;
using Zenject;
using Sirenix.OdinInspector;

namespace Project.Client.Src.com.AB.GamePlay.Common.Audio
{
    public class AudioInstaller : MonoInstaller
    {
        [InlineEditor] [SerializeField] ScriptableObject[] _mappers;
        IAudioMapper[] Mappers => _mappers.Select(item => (IAudioMapper)item).ToArray();
        [SerializeField] Setup Settings;

        public override void InstallBindings()
        {
            Container.BindInstance(transform).WithId(AudioConst.AUDIO_CONTAINER_KEY);

            Settings.Sound.Mappers = Mappers;
            Container.BindInstance(Settings.Player);
            Container.BindInstances(Settings.Sound);
            Container.BindInstance(Settings.Music);

            Container.Bind<AudioPlayerService>().AsSingle();
            Container.Bind<IAudioSFXService>().To<AudioSFXService>().AsSingle();
            Container.Bind<AudioMusicService>().AsSingle().NonLazy();

            Container.BindFactory<AudioPlayerMono, AudioPlayerMono.Factory>()
                .WithId(AudioPlayerService.Settings.MUSIC_PLAYER_FACTORY_KEY)
                .FromComponentInNewPrefab(Settings.Player.MusicPlayerPrefab)
                .UnderTransform(transform);

            Container.BindMemoryPool<AudioPlayerMono, AudioPlayerMono.Pool>()
                .WithId(AudioPlayerService.Settings.SFX_PLAYER_POOL_KEY)
                .WithInitialSize(Settings.Player.NumberSFXPlayerInitial)
                .WithMaxSize(Settings.Player.NumberSFXPlayersMax)
                .FromComponentInNewPrefab(Settings.Player.SFXPlayerPrefab)
                .UnderTransform(transform);
        }

        [Serializable]
        public class Setup
        {
            public AudioSFXService.Setting Sound;
            public AudioMusicService.Settings Music;
            public AudioPlayerService.Settings Player;
        }
    }
}