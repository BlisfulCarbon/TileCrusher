using Project.Client.Src.com.AB.GamePlay.Common.Const;
using Project.Client.Src.com.AB.GamePlay.DigGame.Map;
using UnityEngine;
using Zenject;

namespace Project.Client.Src.com.AB.GamePlay.DigGame
{
    [CreateAssetMenu(
        fileName = "DigGame$Name$Def", 
        menuName = EditorConst.ASSET_MENU_DIG_GAME + "DigDef")]
    public class DigGameSoInstaller : ScriptableObjectInstaller
    {
        public MapGamePlayLayersService.Settings GamePlay;

        public override void InstallBindings()
        {
            Container.BindInstance(GamePlay).IfNotBound();
        }
    }
}