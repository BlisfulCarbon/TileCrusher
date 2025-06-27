using Project.Client.Src.com.AB.GamePlay.DigGame.Map;
using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Logic
{
    public class LogicMapTileBreakHandler : DigGameLogicInteractionHandler
    {
        readonly IMapService _map;

        public LogicMapTileBreakHandler(LogicSession logicSession, IMapService map)
        {
            _map = map;
            SetSession(logicSession);
        }

        public override bool Handle(Vector2 position)
        {
            if (_session.MinedWasBreak)
                return base.Handle(position);

            (_session.TileWasBreak, _session.TileWasBroken) = _map.Break(
                _session.MapCell.GridPosition,
                out var reaction);

            if (_session.TileWasBreak)
                _session.Reaction = reaction;

            return base.Handle(position);
        }
    }
}