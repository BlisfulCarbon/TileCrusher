using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.Common.Audio
{
    public class AudioService
    {
        readonly Dictionary<string, AudioEntry> _audios = new();
        readonly AudioPlayerService _playerService;
        readonly Setting _setting;
        int _playListCurrentTrack;

        public AudioService(Setting settings, AudioPlayerService playerService)
        {
            _setting = settings;
            _playerService = playerService;

            foreach (var mapper in settings.Mappers)
            foreach (var audio in mapper.GetAudiosMapping())
            {
                Debug.Log($"Audio:: {audio.Def.ID}");

                _audios.Add(audio.Key, new AudioEntry(audio.Def));
            }
            
            PlayNextTrackAsync().Forget();
        }


        async UniTaskVoid PlayNextTrackAsync()
        {
            var playlist = _setting.PlayList;
            var musicPlayer = _playerService.GetMusicPlayer();
            
            if (playlist.Count == 0)
                return;

            if (_playListCurrentTrack >= playlist.Count)
                _playListCurrentTrack = 0;
            
            var clip = playlist[_playListCurrentTrack];
            musicPlayer.clip = clip;
            musicPlayer.Play();

            await UniTask.WaitUntil(() => !musicPlayer.isPlaying);
            await UniTask.Yield();
            PlayNextTrackAsync().Forget();
        }
        
        class AudioEntry
        {
            public readonly AudioSo Def;

            public AudioEntry(AudioSo def) =>
                Def = def;
        }

        public void Play(string key)
        {
            AudioSource player = _playerService.GetSFXPlayer();
            player.clip = _audios[key].Def.Audio;
            player.Play();
        }

        [Serializable]
        public class Setting
        {
            public IAudioMapper[] Mappers;

            public Setting(IAudioMapper[] mappers) => Mappers = mappers;
            public List<AudioClip> PlayList;
        }
    }
}