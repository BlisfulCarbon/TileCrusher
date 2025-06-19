using System.Collections.Generic;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Mined
{
    public interface IMinedMapper
    {
        public IEnumerable<MinedMappingDto> GetParticleMapping();
    }
}