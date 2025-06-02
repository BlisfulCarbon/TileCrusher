using System;
using UniRx;
using UnityEngine;

namespace Project.Client.Src.com.AB.Infrastructure.InfrastructureAPI.Input
{
    public interface IInputService
    {
        IReadOnlyReactiveProperty<Vector2> TouchPosition { get; }
        IObservable<Vector2> OnTap { get; }
        IObservable<Vector2> OnHold { get; }
        IObservable<Vector2> OnSwipe { get; }
    }
}