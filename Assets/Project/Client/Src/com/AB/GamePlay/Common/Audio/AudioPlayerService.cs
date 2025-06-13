using System;
using UnityEngine;
using Zenject;

namespace Project.Client.Src.com.AB.GamePlay.Common.Audio
{
    public class AudioPlayerService
    {
        Transform _container;
        Settings _settings;

        AudioSource _sfxPlayer;
        AudioSource _musicPlayer;

        public AudioPlayerService([Inject(Id = AudioConst.AUDIO_CONTAINER_KEY)] Transform audioContainer,
            Settings settings)
        {
            var goSFX = new GameObject("PlayerSFX");
            goSFX.transform.parent = _container;
            _sfxPlayer = goSFX.AddComponent<AudioSource>();

            var goMusic = new GameObject("PlayerMusic");
            goMusic.transform.parent = _container;
            _musicPlayer = goMusic.AddComponent<AudioSource>();
        }

        public AudioSource GetMusicPlayer() => 
            _musicPlayer;

        public AudioSource GetSFXPlayer() => 
            _sfxPlayer;

        [Serializable]
        public class Settings
        {
            public int NumberFXPlayers;
        }
    }
}