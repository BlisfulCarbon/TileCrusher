using System.Collections.Generic;

namespace Project.Client.Src.com.AB.GamePlay.Common.Particles
{
    public class ParticlePoolRef
    {
        public readonly Dictionary<string, (ParticleMono.Pool, ParticleSo)> Items = new ();
    }
}