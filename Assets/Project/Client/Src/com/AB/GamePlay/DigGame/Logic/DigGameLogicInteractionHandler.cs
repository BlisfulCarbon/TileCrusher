using Project.Client.Src.com.AB.GamePlay.Common.Interaction;
using Project.Client.Src.com.AB.GamePlay.DigGame.Map;
using Project.Client.Src.com.AB.GamePlay.DigGame.React;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Logic
{
    public class DigGameLogicInteractionHandler : InteractionHandlerBase
    {
        protected Session _session = new ();

        protected void SetSession(Session session) => _session = session;

        public class Session
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