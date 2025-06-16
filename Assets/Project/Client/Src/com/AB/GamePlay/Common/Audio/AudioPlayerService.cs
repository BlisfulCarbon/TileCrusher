using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Project.Client.Src.com.AB.GamePlay.Common.Audio
{
    public class AudioPlayerService
    {
        Transform _container;
        Settings _settings;

        AudioPlayerMono _musicPlayer;
        AudioPlayerMono.Pool _sfxPlayerPool;

        public AudioPlayerService(
            Settings settings,
            [Inject(Id = AudioConst.AUDIO_CONTAINER_KEY)]
            Transform audioContainer,
            [Inject(Id = Settings.MUSIC_PLAYER_FACTORY_KEY)]
            AudioPlayerMono.Factory musicPlayerFactory,
            [Inject(Id = Settings.SFX_PLAYER_POOL_KEY)]
            AudioPlayerMono.Pool sfxPlayerPool
        )
        {
            CreateMusicPlayer(musicPlayerFactory);
            CreateSFXPlayers(sfxPlayerPool);
        }

        public async UniTaskVoid PlaySFX(AudioClip audio)
        {
            AudioPlayerMono player = _sfxPlayerPool.Spawn();
            await player.PlayAsync(audio);
            _sfxPlayerPool.Despawn(player);
        }

        public AudioPlayerMono GetMusicPlayer() =>
            _musicPlayer;

        void CreateSFXPlayers(AudioPlayerMono.Pool sfxPlayerPool)
        {
            _sfxPlayerPool = sfxPlayerPool;
        }

        void CreateMusicPlayer(AudioPlayerMono.Factory musicPlayerFactory)
        {
            _musicPlayer = musicPlayerFactory.Create();
            _musicPlayer.name = Settings.MUSIC_PLAYER_KEY;
        }

        [Serializable]
        public class Settings
        {
            public int NumberSFXPlayerInitial;
            public int NumberSFXPlayersMax;
            public const string SFX_PLAYER_KEY = "SFXPlayer";
            public const string SFX_PLAYER_POOL_KEY = "SFXPlayer";
            public AudioPlayerMono SFXPlayerPrefab;

            public const string MUSIC_PLAYER_KEY = "MusicPlayer";
            public const string MUSIC_PLAYER_FACTORY_KEY = "MusicPlayerFactory";
            public AudioPlayerMono MusicPlayerPrefab;
        }
    }
}