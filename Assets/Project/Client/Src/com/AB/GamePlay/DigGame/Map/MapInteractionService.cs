using System;
using Cysharp.Threading.Tasks;
using Project.Client.Src.com.AB.Infrastructure.InfrastructureAPI;
using Project.Client.Src.com.AB.Infrastructure.InfrastructureAPI.Input;
using UniRx;
using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Map
{
    public class MapInteractionService : IDisposable
    {
        readonly IInputService _input;
        readonly CompositeDisposable _disposables = new();

        public MapInteractionService(IInputService input)
        {
            _input = input;

            _input.OnTap.Subscribe(OnTap).AddTo(_disposables);
            _input.OnHold.Subscribe(OnHold).AddTo(_disposables);
            _input.OnSwipe.Subscribe(OnSwipe).AddTo(_disposables);
        }

        void OnTap(Vector2 position)
        {
            Debug.Log($"MapInteractionService::Tap {position}");
        }

        void OnHold(Vector2 position)
        {
            Debug.Log($"MapInteractionService::OnHold {position}");
        }

        void OnSwipe(Vector2 position)
        {
            Debug.Log($"MapInteractionService::OnSwipe {position}");
        }

        public void Dispose() =>
            _disposables.Dispose();
    }
}