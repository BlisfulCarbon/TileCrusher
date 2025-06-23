using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Project.Client.Src.com.AB.GamePlay.Common.Audio;
using Project.Client.Src.com.AB.GamePlay.Common.Particles;
using Sirenix.OdinInspector;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.React
{
    [Serializable]
    public class ReactList
    {
        [SerializedDictionary("Key", "React")] public SerializedDictionary<Reacts, ReactSo> Items;

        [OnInspectorInit]
        void Init()
        {
            if(Items.Count > 0)
                return;
            
            Items.Add(Reacts.Break, null);
            Items.Add(Reacts.Broken, null);
        }

        public ReactSo Get(Reacts key) => 
            Items[key];
        
        public IEnumerable<AudioDto> GetAudioMapping() =>
            Items.Select(item => AudioDto.FromDef(item.Value.Audio));

        public IEnumerable<ParticleDto> GetParticleMapping() => 
            Items.Select(item => ParticleDto.FromDef(item.Value.Particle));
    }
}