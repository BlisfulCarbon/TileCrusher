using System;
using Cysharp.Threading.Tasks;
using Project.Client.Src.com.AB.Infrastructure.InfrastructureAPI;
using Project.Client.Src.com.AB.Infrastructure.InfrastructureAPI.Input;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Project.Client.Src.com.AB.Infrastructure.Input
{
    public class InputService : IInputService, IInitializable, IDisposable
    {
        public InputService() =>
            _gameControls = new GameControls();

        const float SWIPE_THRESHOLD = 10f;
        const float LONG_PRESS_THRESHOLD = 0.6f;
        readonly CompositeDisposable _disposables = new();
        readonly GameControls _gameControls;
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
            _gameControls.Enable();

            GetPosition();

            BeginningTouch();
            Lowering();
            UpdateState();
        }

        void GetPosition()
        {
            _gameControls.Touch.Touch
                .OnPerformedAsObservable()
                .Select(ctx => ctx.ReadValue<Vector2>())
                .Subscribe(pos => _touchPosition.Value = pos)
                .AddTo(_disposables);
        }

        void BeginningTouch()
        {
            _gameControls.Touch.Press
                .OnStartedAsObservable()
                .Subscribe(_ =>
                {
                    _isTouching = true;
                    _startTime = Time.time;
                    _lastPosition = _touchPosition.Value;
                    _swipeTriggered = false;
                    _holdTriggered = false;
                })
                .AddTo(_disposables);
        }

        void Lowering()
        {
            _gameControls.Touch.Press
                .OnCanceledAsObservable()
                .Subscribe(_ =>
                {
                    if (!_swipeTriggered && !_holdTriggered)
                        _onTap.OnNext(_touchPosition.Value);

                    _isTouching = false;
                })
                .AddTo(_disposables);
        }

        void UpdateState()
        {
            Observable.EveryUpdate()
                .Where(_ => _isTouching)
                .Subscribe(_ =>
                {
                    var currentPosition = _touchPosition.Value;
                    float distance = (currentPosition - _lastPosition).sqrMagnitude;
                    _lastPosition = currentPosition;
                    
                    if (distance > SWIPE_THRESHOLD)
                    {
                        _swipeTriggered = true;
                        _onSwipe.OnNext(currentPosition);

                        _holdTriggered = false;
                        _startTime = Time.time;
                        
                        return;
                    }

                    if (!_holdTriggered && (Time.time - _startTime > LONG_PRESS_THRESHOLD)) 
                        _holdTriggered = true;

                    if (_holdTriggered) 
                        _onHold.OnNext(currentPosition);

                })
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _gameControls?.Dispose();
            _disposables.Dispose();
        }
    }
}