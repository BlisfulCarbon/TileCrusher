using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Project.Client.Src.com.AB.GamePlay.Common.Audio
{
    public class AudioPlayerMono : MonoBehaviour
    {
        [SerializeField] AudioSource _source;
        public bool IsPlaying => _source.isPlaying;
        public CancellationTokenSource _cts;

        public async UniTask PlayAsync(AudioClip clip)
        {
            Stop();

            _cts = new CancellationTokenSource();
            
            _source.clip = clip;
            _source.Play();

            await UniTask.Delay(
                TimeSpan.FromSeconds(clip.length), false,
                PlayerLoopTiming.Update, _cts.Token);

            if (IsPlaying)
                Stop();
        }

        public void Stop()
        {
            if (IsPlaying)
                _source.Stop();

            _source.clip = null;
            
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        void OnDestroy() =>
            Stop();

        public class Factory : PlaceholderFactory<AudioPlayerMono>
        {
        }

        public class Pool : MemoryPool<AudioPlayerMono>
        {
        }
    }
}