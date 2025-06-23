using Plugins.PaperCrafts.com.AB.Extensions;
using Project.Client.Src.com.AB.GamePlay.DigGame.Mined;
using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Logic
{
    public class LogicMinedSpawnHandler : DigGameLogicInteractionHandler
    {
        readonly IMinedService _mined;

        public LogicMinedSpawnHandler(Session session, IMinedService mined)
        {
            _mined = mined;
            SetSession(session);
        }

        public override bool Handle(Vector2 position)
        {
            if (_session.TileWasBroken)
                _mined.TrySpawn(
                    _session.MapCell.GridPosition.IncreaseZ(1),
                    _session.MapCell.WorldPosition);

            return true;
        }
    }
}