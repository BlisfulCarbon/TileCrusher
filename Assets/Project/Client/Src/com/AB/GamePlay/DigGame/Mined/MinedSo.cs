using Project.Client.Src.com.AB.GamePlay.Common.Defs;
using Project.Client.Src.com.AB.GamePlay.DigGame.React;
using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Mined
{
    [CreateAssetMenu(
        fileName = "DigGameMined$Name$Def",
        menuName = DigGameConst.ASSET_MENU_DIGGAME_PATH + "MinedDef")]
    public class MinedSo : DefsBase
    {
        public MinedMono Prefab;
        public int BreakCountMax;

        public ReactList Actions;
    }
}