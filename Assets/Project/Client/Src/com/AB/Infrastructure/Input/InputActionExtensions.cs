using System;
using UniRx;
using UnityEngine.InputSystem;

namespace Project.Client.Src.com.AB.Infrastructure.Input
{
    public static class InputActionExtensions
    {
        public static IObservable<InputAction.CallbackContext> OnPerformedAsObservable(this InputAction action)
        {
            return Observable.FromEvent<InputAction.CallbackContext>(
                h => action.performed += h,
                h => action.performed -= h
            );
        }
    
        public static IObservable<InputAction.CallbackContext> OnStartedAsObservable(this InputAction action)
        {
            return Observable.FromEvent<InputAction.CallbackContext>(
                h => action.started += h,
                h => action.started -= h
            );
        }
    
        public static IObservable<InputAction.CallbackContext> OnCanceledAsObservable(this InputAction action)
        {
            return Observable.FromEvent<InputAction.CallbackContext>(
                h => action.canceled += h,
                h => action.canceled -= h
            );
        }
    }
}