using System.Collections.Generic;
using Project.Client.Src.com.AB.GamePlay.Common.Const;
using Project.Client.Src.com.AB.GamePlay.Common.Particles;
using Project.Client.Src.com.AB.GamePlay.DigGame.Map;
using UnityEngine;
using Zenject;

namespace Project.Client.Src.com.AB.GamePlay.DigGame
{
    [CreateAssetMenu(
        fileName = "DigGame$Name$Def",
        menuName = AssetsConst.ASSET_MENU_DIG_GAME + "DigDef")]
    public class DigGameSoInstaller : ScriptableObjectInstaller<DigGameSoInstaller>, IParticleMapper
    {
        public MapGamePlayService.Settings GamePlay;
        public MapInteractionService.Settings MapInteraction;

        public override void InstallBindings()
        {
            Container.BindInstance(GamePlay).IfNotBound();
            Container.BindInstance(MapInteraction).IfNotBound();
        }

        public IEnumerable<ParticleMappingItem> GetParticleMapping() => 
            GamePlay.GetParticleMapping();
    }
}