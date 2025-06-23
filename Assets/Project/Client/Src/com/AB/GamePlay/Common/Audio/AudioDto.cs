using System;

namespace Project.Client.Src.com.AB.GamePlay.Common.Audio
{
    [Serializable]
    public class AudioDto
    {
        public readonly string Key;
        public readonly AudioSo Def;

        public AudioDto(string key, AudioSo def)
        {
            Key = key;
            Def = def;
        }

        public static AudioDto FromDef(AudioSo def) => 
            new(def.ID, def);
    }
}