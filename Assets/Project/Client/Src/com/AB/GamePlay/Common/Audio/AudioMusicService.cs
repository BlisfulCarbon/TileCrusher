using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.Common.Audio
{
    public class AudioMusicService : IDisposable
    {
        readonly AudioMusicPlayList _playList;
        readonly AudioPlayerMono _player;

        public AudioMusicService(Settings settings, AudioPlayerService player)
        {
            _player = player.GetMusicPlayer();
            _playList = new AudioMusicPlayList(settings.PlayList);

            PlayTracksAsync(_playList).Forget();
        }

        public async UniTaskVoid PlayTracksAsync(AudioMusicPlayList playList)
        {
            AudioClip track = playList.GetNewTrack();
            await _player.PlayAsync(track);
            PlayTracksAsync(playList).Forget();
        }

        public void StopTracks() =>
            _player.Stop();

        public void Dispose() => 
            StopTracks();

        [Serializable]
        public class Settings
        {
            public List<AudioClip> PlayList;
        }

        public class AudioMusicPlayList
        {
            public readonly List<AudioClip> Audios;
            public int CurrentTrack;

            public AudioMusicPlayList(List<AudioClip> audios) =>
                Audios = audios;

            public AudioClip GetNewTrack()
            {
                if (CurrentTrack >= Audios.Count)
                    CurrentTrack = 0;

                return Audios[CurrentTrack++];
            }
        }
    }
}