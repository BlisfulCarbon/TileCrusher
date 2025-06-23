using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.Common.Interaction
{
    public abstract class InteractionHandlerBase : IInteractionHandler
    {
        protected IInteractionHandler _next;

        public void SetNext(IInteractionHandler next) => _next = next;

        public virtual bool Handle(Vector2 position)
        {
            if (_next != null)
                return _next.Handle(position);

            return true;
        }
    }
}