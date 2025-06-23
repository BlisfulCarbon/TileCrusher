using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.React
{
    public interface IReactService
    {
        public IReact GetReact(ReactList list, bool broken);
        void Produce(IReact react, Vector2 world);
    }
}