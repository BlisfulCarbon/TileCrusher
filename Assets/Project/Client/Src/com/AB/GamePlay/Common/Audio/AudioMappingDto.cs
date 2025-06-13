using System;

namespace Project.Client.Src.com.AB.GamePlay.Common.Audio
{
    [Serializable]
    public class AudioMappingDto
    {
        public readonly string Key;
        public readonly AudioSo Def;

        public AudioMappingDto(string key, AudioSo def)
        {
            Key = key;
            Def = def;
        }
    }
}