using Project.Client.Src.com.AB.GamePlay.Common.Audio;
using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Mined
{
    [CreateAssetMenu(
        fileName = "$Name$MinedDef",
        menuName = DigGameConst.ASSET_MENU_DIGGAME_PATH + "MinedDef")]
    public class MinedSo : ScriptableObject
    {
        public string ID;
        public MinedMono Prefab;
        public AudioSo HittinSFX;
        public AudioSo CollectingSFX;
    }
}