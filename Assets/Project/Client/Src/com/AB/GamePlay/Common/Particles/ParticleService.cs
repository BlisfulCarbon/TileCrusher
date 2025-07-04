using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.Common.Particles
{
    public class ParticleService
    {
        readonly Dictionary<string, ParticleEntry> _pools = new();

        public ParticleService(Dictionary<string, (ParticleMono.Pool, ParticleSo)> pools)
        {
            foreach (var item in pools)
                _pools.Add(item.Key, new ParticleEntry(item.Value.Item1, item.Value.Item2));
        }

        class ParticleEntry
        {
            public ParticleEntry(ParticleMono.Pool pool, ParticleSo def)
            {
                Pool = pool;
                Def = def;
            }

            public ParticleSo Def;
            public ParticleMono.Pool Pool;
        }

        public void Spawn(string key, Vector3 position)
        {
            var entry = _pools[key];
            var particle = entry.Pool.Spawn(position);

            DespawnAsync(entry, particle).Forget();
        }

        async UniTaskVoid DespawnAsync(ParticleEntry entry, ParticleMono particle)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(entry.Def.LifeTime));

            entry.Pool.Despawn(particle);
        }
    }
}