using System;

namespace Project.Client.Src.com.AB.GamePlay.Common.Particles
{
    [Serializable]
    public class ParticleDto
    {
        public readonly string Key;
        public readonly ParticleSo Def;
        
        public ParticleDto(string key, ParticleSo def)
        {
            Key = key;
            Def = def;
        }

        public static ParticleDto FromDef(ParticleSo def) => 
            new(def.ID, def);
    }
}