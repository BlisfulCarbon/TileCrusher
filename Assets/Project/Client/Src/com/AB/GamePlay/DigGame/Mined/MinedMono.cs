using System;
using DG.Tweening;
using Plugins.PaperCrafts.com.AB.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Mined
{
    public class MinedMono : MonoBehaviour
    {
        public SpriteRenderer Sprite;
        public Animation Animations = new Animation();
        public int HitCount { get; private set; }
        

        public void Hit()
        {
            HitCount++;
            AnimationShake();
        }

        [Button]
        public void AnimationShake() => Animations.Shake(Sprite.transform);

        [Serializable]
        public class Animation
        {
            public float ShakeDuration = 0.3f; 
            public float Strength = 1f; 
            public int Vibrato = 10; 

            Sequence _tween;

            public void Shake(Transform sorce)
            {
                if (_tween != null) _tween.Kill(true);

                Sequence seq = DOTween.Sequence();
                seq.Append(sorce.DOShakePosition(ShakeDuration, Strength, Vibrato));
                seq.SetLoops(3, LoopType.Restart);
            }

            void OnDestroy()
            {
                if (_tween != null && _tween.IsActive()) _tween.Kill();
            }
        }

        public class Pool : MemoryPool<Vector3, MinedMono>
        {
            protected override void Reinitialize(Vector3 position, MinedMono item) => 
                item.SetPosition(position);
        }
    }
}