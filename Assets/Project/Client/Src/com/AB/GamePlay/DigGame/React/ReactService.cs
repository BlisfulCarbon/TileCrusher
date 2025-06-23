using Project.Client.Src.com.AB.GamePlay.Common.Audio;
using Project.Client.Src.com.AB.GamePlay.Common.Particles;
using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.React
{
    public class ReactService : IReactService
    {
        readonly IAudioSFXService _audio;
        readonly IParticleService _particle;

        public ReactService(IAudioSFXService audio, IParticleService particle) =>
            (_audio, _particle) = (audio, particle);
        
        public IReact GetReact(ReactList list, bool broken) =>
            list.Get(broken ? Reacts.Broken : Reacts.Break);

        public void Produce(IReact react, Vector2 world)
        {
            _audio.Play(react.GetAudioKey());
            _particle.Spawn(react.GetParticleKey(), world);
        }
    }
}