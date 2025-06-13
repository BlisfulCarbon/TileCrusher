using System.Collections.Generic;

namespace Project.Client.Src.com.AB.GamePlay.Common.Particles
{
    public interface IParticleMapper
    {
        public IEnumerable<ParticleMappingDto> GetParticleMapping();
    }
}