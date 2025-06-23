using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.Common.Audio
{
    public class AudioSFXService : IAudioSFXService
    {
        readonly Dictionary<string, AudioSoundEntry> _audios = new();
        readonly AudioPlayerService _playerService;
        readonly Setting _settings;
        int _playListCurrentTrack;

        public AudioSFXService(Setting settings, AudioPlayerService playerService)
        {
            _settings = settings;
            _playerService = playerService;

            InitializeSounds(_settings.Mappers, _settings.Debugg);
        }

        void InitializeSounds(IAudioMapper[] mappers, bool debug = false)
        {
            foreach (var mapper in mappers)
            foreach (var audio in mapper.GetAudiosMapping())
            {
                if (debug)
                    Debug.Log($"{nameof(AudioSFXService)}::Initialize: audio: {audio.Def.ID}, with key: {audio.Key}");

                if (_audios.ContainsKey(audio.Key))
                    continue;

                _audios.Add(audio.Key, new AudioSoundEntry(audio.Def));
            }
        }

        public void Play(string key)
        {
            if (!_audios.ContainsKey(key))
            {
                Debug.LogWarning($"{nameof(AudioSFXService)}::Play => can not find audio clip with key: {key}");
                return;
            }

            _playerService.PlaySFX(_audios[key].Def.Audio);
        }

        class AudioSoundEntry
        {
            public readonly AudioSo Def;

            public AudioSoundEntry(AudioSo def) =>
                Def = def;
        }

        [Serializable]
        public class Setting
        {
            public bool Debugg;

            public IAudioMapper[] Mappers;
            public Setting(IAudioMapper[] mappers) => Mappers = mappers;
        }
    }
}