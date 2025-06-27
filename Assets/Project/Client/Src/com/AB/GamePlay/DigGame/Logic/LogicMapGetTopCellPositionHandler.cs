using Project.Client.Src.com.AB.GamePlay.DigGame.Map;
using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Logic
{
    public class LogicMapGetTopCellPositionHandler : DigGameLogicInteractionHandler
    {
        readonly IMapService _map;

        public LogicMapGetTopCellPositionHandler(LogicSession logicSession, IMapService map)
        {
            _map = map;
            SetSession(logicSession);
        }

        public override bool Handle(Vector2 position)
        {
            _session.MapCell = _map.GetTopCell(position);
            
            return base.Handle(position);
        }
    }
}