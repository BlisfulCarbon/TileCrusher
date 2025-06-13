using System;

namespace Project.Client.Src.com.AB.GamePlay.Common.Particles
{
    [Serializable]
    public class ParticleMappingDto
    {
        public readonly string Key;
        public readonly ParticleSo Def;
        
        public ParticleMappingDto(string key, ParticleSo def)
        {
            Key = key;
            Def = def;
        }
    }
}