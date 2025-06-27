using Project.Client.Src.com.AB.GamePlay.DigGame.Mined;
using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Logic
{
    public class LogicMinedBreakHandler : DigGameLogicInteractionHandler
    {
        readonly IMinedService _mined;

        public LogicMinedBreakHandler(LogicSession logicSession, IMinedService mined)
        {
            _mined = mined;
            SetSession(logicSession);
        }

        public override bool Handle(Vector2 position)
        {
            _session.MinedWasBreak = _mined.Break(
                _session.MapCell.GridPosition,
                out var reaction);

            if (_session.MinedWasBreak)
                _session.Reaction = reaction;

            return base.Handle(position);
        }
    }
}