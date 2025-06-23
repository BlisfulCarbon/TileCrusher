using System.Collections.Generic;
using System.Linq;
using Project.Client.Src.com.AB.GamePlay.Common.Audio;
using Project.Client.Src.com.AB.GamePlay.Common.Const;
using Project.Client.Src.com.AB.GamePlay.Common.Particles;
using Project.Client.Src.com.AB.GamePlay.DigGame.Logic;
using Project.Client.Src.com.AB.GamePlay.DigGame.Map;
using Project.Client.Src.com.AB.GamePlay.DigGame.Mined;
using UnityEngine;
using Zenject;

namespace Project.Client.Src.com.AB.GamePlay.DigGame
{
    [CreateAssetMenu(
        fileName = "DigGame$Name$Def",
        menuName = AssetsConst.ASSET_MENU_GAMEPALAY_PATH + "DigGame/DigGameDef")]
    public class DigGameSoInstaller : ScriptableObjectInstaller<DigGameSoInstaller>, IParticleMapper, IAudioMapper
    {
        public MinedService.Settings Mined;
        public MapService.Settings GamePlay;
        public DigGameLogicService.Settings Logic;

        public override void InstallBindings()
        {
            Container.BindInstance(Mined);
            Container.BindInstance(GamePlay).IfNotBound();
            Container.BindInstance(Logic).IfNotBound();
        }

        public IEnumerable<ParticleDto> GetParticleMapping()
        {
            return GamePlay.GetParticleMapping().Concat(Mined.GetParticleMapping());
        }

        public IEnumerable<AudioDto> GetAudiosMapping() =>
            GamePlay.GetAudiosMapping().Concat(Mined.GetAudiosMapping());
    }
}