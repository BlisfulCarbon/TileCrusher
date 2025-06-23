using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Logic
{
    public class LogicResetSessionHandler : DigGameLogicInteractionHandler
    {
        public LogicResetSessionHandler(Session session) => 
            SetSession(session);

        public override bool Handle(Vector2 position)
        {
            _session.Reset();
            return base.Handle(position);
        }
    }
}