using Project.Client.Src.com.AB.GamePlay.Common.Interaction;
using Project.Client.Src.com.AB.GamePlay.DigGame.Map;
using Project.Client.Src.com.AB.GamePlay.DigGame.React;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Logic
{
    public class DigGameLogicInteractionHandler : InteractionHandlerBase
    {
        protected LogicSession _session = new ();

        protected void SetSession(LogicSession logicSession) => _session = logicSession;

        public class LogicSession
        {
            public MapCell MapCell ;
            
            public bool TileWasBroken;
            public bool TileWasBreak;
            public bool MinedWasBreak;
            public IReact Reaction; 
            
            public void Reset()
            {
                MapCell = new MapCell();

                TileWasBreak = false;
                TileWasBroken = false;
                MinedWasBreak = false;
                Reaction = null;
            }
        }
    }
}