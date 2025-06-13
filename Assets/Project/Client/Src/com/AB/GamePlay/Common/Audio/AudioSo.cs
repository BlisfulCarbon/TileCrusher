using UnityEngine;
using Project.Client.Src.com.AB.GamePlay.Common.Const;

namespace Project.Client.Src.com.AB.GamePlay.Common.Audio
{
    [CreateAssetMenu(
        fileName = "$Name$AudioDef",
        menuName = AssetsConst.ASSET_MENU_AUDIO_PATH)]
    public class AudioSo : ScriptableObject
    {
        public string ID;
        public AudioClip Audio;
        public AudioLayer Layer = AudioLayer.SFX;

        void OnValidate()
        {
            if (string.IsNullOrEmpty(ID)) ID = Audio.name;
        }
    }
}