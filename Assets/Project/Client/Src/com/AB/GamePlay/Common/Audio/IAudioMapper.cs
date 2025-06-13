using System.Collections.Generic;

namespace Project.Client.Src.com.AB.GamePlay.Common.Audio
{
    public interface IAudioMapper
    {
        public IEnumerable<AudioMappingDto> GetAudiosMapping();
    }
}