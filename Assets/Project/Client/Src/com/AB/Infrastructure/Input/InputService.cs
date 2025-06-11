using System;
using System.Linq;
using Project.Client.Src.com.AB.Infrastructure.InfrastructureAPI.Input;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using Zenject;

namespace Project.Client.Src.com.AB.Infrastructure.Input
{
    public class InputService : IInputService, IInitializable, IDisposable
    {
        public InputService(Settings settings)
        {
            _settings = settings;
            _gameInput = new GameInput();

            debug = new();
        }

        public readonly Debugger debug;

        readonly Settings _settings;
        readonly CompositeDisposable _disposables = new();
        readonly GameInput _gameInput;
        readonly ReactiveProperty<Vector2> _touchPosition = new();
        readonly Subject<Vector2> _onTap = new();
        readonly Subject<Vector2> _onHold = new();
        readonly Subject<Vector2> _onSwipe = new();

        public IReadOnlyReactiveProperty<Vector2> TouchPosition => _touchPosition;
        public IObservable<Vector2> OnTap => _onTap;
        public IObservable<Vector2> OnHold => _onHold;
        public IObservable<Vector2> OnSwipe => _onSwipe;

        bool _isTouching;
        bool _swipeTriggered;
        bool _holdTriggered;

        float _startTime;
        Vector2 _lastPosition;


        public void Initialize()
        {
            _gameInput.Enable();

            GetPosition();

            BeginningTouch();
            Lowering();
            UpdateState();


            Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(_ =>
            {
                var pointer = InputSystem.devices.FirstOrDefault(d => d is Pointer);
                if (pointer != null)
                {
                    Debug.Log($"Pointer found: {pointer.displayName}");
                }
                else
                {
                    Debug.Log($"Pointer not found");
                }
            });
        }

        void GetPosition()
        {
            _gameInput.GamePlay.Tap
                .OnPerformedAsObservable()
                .Select(ctx => ctx.ReadValue<Vector2>())
                .Subscribe(pos => _touchPosition.Value = pos)
                .AddTo(_disposables);
        }

        void BeginningTouch()
        {
            _gameInput.GamePlay.Press
                .OnStartedAsObservable()
                .DelayFrame(1) // Race condition with touch 
                .Subscribe(_ =>
                {
                    Debug.Log("InputService::Press::Begin");

                    _lastPosition = _touchPosition.Value;
                    _startTime = Time.time;
                    _swipeTriggered = false;
                    _holdTriggered = false;
                    _isTouching = true;
                })
                .AddTo(_disposables);
        }

        void Lowering()
        {
            _gameInput.GamePlay.Press
                .OnCanceledAsObservable()
                .Subscribe(_ =>
                {
                    Debug.Log("InputService::Press::Lowering");

                    if (!_swipeTriggered && !_holdTriggered)
                        _onTap.OnNext(_touchPosition.Value);

                    _isTouching = false;
                })
                .AddTo(_disposables);
        }

        void UpdateState()
        {
            return;

            Observable.EveryUpdate()
                .Where(_ => _isTouching)
                .Subscribe(_ =>
                {
                    Touchscreen.current.primaryTouch.position.ReadValue();

                    var currentPosition = _touchPosition.Value;
                    float distance = (_lastPosition - currentPosition).sqrMagnitude;

                    _lastPosition = currentPosition;

                    if (distance > _settings.SwipeThreshold)
                    {
                        _swipeTriggered = true;
                        _onSwipe.OnNext(currentPosition);

                        _holdTriggered = false;
                        _startTime = Time.time;

                        return;
                    }

                    if (!_holdTriggered && (Time.time - _startTime > _settings.LongPressThreshold))
                        _holdTriggered = true;

                    if (_holdTriggered)
                        _onHold.OnNext(currentPosition);
                })
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _gameInput?.Dispose();
            _disposables.Dispose();
        }

        [Serializable]
        public class Settings
        {
            public float SwipeThreshold = 20f;
            public float LongPressThreshold = 0.6f;
        }

        public class Debugger
        {
            public void CheckDevices()
            {
                foreach (var device in InputSystem.devices)
                {
                    Debug.Log($"[Device] {device.name} — {device.enabled} — {device.added}");
                }
            }
        }
    }
}