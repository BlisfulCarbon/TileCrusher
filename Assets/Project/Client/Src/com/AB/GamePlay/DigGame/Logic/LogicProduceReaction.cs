using Project.Client.Src.com.AB.GamePlay.DigGame.React;
using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Logic
{
    public class LogicProduceReaction : DigGameLogicInteractionHandler
    {
        public IReactService _react;

        public LogicProduceReaction(Session session, IReactService react) =>
            (_react, _session) = (react, session);

        public override bool Handle(Vector2 position)
        {
            if (_session.Reaction != null)
                _react.Produce(_session.Reaction, _session.MapCell.WorldPosition);

            return base.Handle(position);
        }
    }
}