using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.Common.Interaction
{
    public interface IInteractionHandler
    {
        public void SetNext(IInteractionHandler next);
        public bool Handle(Vector2 position);
    }
}