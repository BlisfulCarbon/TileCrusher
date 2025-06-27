using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Logic
{
    public class LogicProvideSessionHandler : DigGameLogicInteractionHandler
    {
        public LogicProvideSessionHandler(LogicSession logicSession) => 
            SetSession(logicSession);

        public override bool Handle(Vector2 position)
        {
            _session.Reset();
            return base.Handle(position);
        }
    }
}