using Project.Client.Src.com.AB.GamePlay.Common.Audio;
using Project.Client.Src.com.AB.GamePlay.Common.Particles;
using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.React
{
    [CreateAssetMenu(
        fileName = "React$Name$Def",
        menuName = DigGameConst.ASSET_MENU_DIGGAME_PATH + "ReactDef")]
    public class ReactSo : ScriptableObject, IReact
    {
        public Reacts ID;
        public AudioSo Audio;
        public ParticleSo Particle;
        
        public string GetAudioKey() => 
            Audio.ID;

        public string GetParticleKey() => 
            Particle.ID;
    }
}