namespace Project.Client.Src.com.AB.GamePlay.Common.Particles
{
    public class ParticleMappingItem
    {
        public readonly string Key;
        public readonly ParticleSo Def;
        
        public ParticleMappingItem(string key, ParticleSo def)
        {
            Key = key;
            Def = def;
        }
    }
}