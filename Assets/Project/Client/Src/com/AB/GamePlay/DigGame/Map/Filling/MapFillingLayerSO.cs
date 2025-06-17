using System;
using System.Collections.Generic;
using Project.Client.Src.com.AB.GamePlay.Common.Audio;
using Project.Client.Src.com.AB.GamePlay.Common.Particles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Map.Filling
{
    [CreateAssetMenu(
        fileName = "$Name$MapFillingLayerDef",
        menuName = DigGameConst.ASSET_MENU_DIGGAME_PATH + "MapFillingDef")]
    public class MapFillingLayerSo : ScriptableObject
    {
        const string BREAK_SUFIX_KEY = "_break";
        const string BROKEN_SUFIX_KEY = "_broken";

        void OnValidate()
        {
            Break.ActionSufixKey = UpdateActionKey(Break.ActionSufixKey, BREAK_SUFIX_KEY);
            Broken.ActionSufixKey = UpdateActionKey(Broken.ActionSufixKey, BROKEN_SUFIX_KEY);
        }

        public string LayerID;
        public List<TileBase> TopologyTiles;

        public LayerAction Break;
        public LayerAction Broken;

        public string GetAudioBreakKey() =>
            Break.Audio.ID;

        public string GetAudioBrokenKey() =>
            Broken.Audio.ID;

        public IEnumerable<AudioMappingDto> GetAudioMapping() =>
            new List<AudioMappingDto>
            {
                new (GetAudioBreakKey(), Break.Audio),
                new (GetAudioBrokenKey(), Broken.Audio),
            };

        public string GetParticleBreakKey() =>
            LayerID + Break.ActionSufixKey;

        public string GetParticleBrokenKey() =>
            LayerID + Broken.ActionSufixKey;

        public IEnumerable<ParticleMappingDto> GetParticleMapping() =>
            new List<ParticleMappingDto>
            {
                new (GetParticleBreakKey(), Break.Particle),
                new (GetParticleBrokenKey(), Broken.Particle),
            };

        string UpdateActionKey(string key, string defaultKey) => 
            string.IsNullOrEmpty(key) ? defaultKey : key;

        [Serializable]
        public class LayerAction
        {
            public AudioSo Audio;
            public ParticleSo Particle;
            public string ActionSufixKey;
        }
    }
}