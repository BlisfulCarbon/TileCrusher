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
        public Animator Animations;
        public int HitCount { get; private set; }

        [SerializeField] string _breakAnimationKey;
        [SerializeField] string _brokenAnimationKey;
        
        [Button]
        public void Break()
        {
            Animations.SetTrigger(_breakAnimationKey);
        }
        
        [Button]
        public void Broken()
        {
            Animations.SetTrigger(_brokenAnimationKey);
        }

        public class Pool : MemoryPool<Vector3, MinedMono>
        {
            protected override void Reinitialize(Vector3 position, MinedMono item) =>
                item.SetPosition(position);
            
            protected override void OnSpawned(MinedMono item) => 
                item.Active(true);

            protected override void OnDespawned(MinedMono item) => 
                item.Active(false);
        }
    }
}