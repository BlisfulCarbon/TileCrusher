using System;
using System.Collections.Generic;
using System.Linq;
using Plugins.PaperCrafts.com.AB.Extensions;
using Project.Client.Src.com.AB.GamePlay.DigGame.Map;
using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Mined
{
    public class MinedService : IMinedService
    {
        readonly Settings _settings;
        readonly MapGamePlayService _mapGamePlay;
        readonly Dictionary<string, MinedEntry> _pools;

        public MinedService(Settings settings, Dictionary<MinedSo, MinedMono.Pool> pools)
        {
            _settings = settings;
            _pools = pools.ToDictionary(item => item.Key.ID, item => new MinedEntry(item.Key, item.Value));
        }

        public bool TrySpawn(Vector2 postion, out MinedMono mined)
        {
            mined = null;
            bool isSpawn = Convert.ToBoolean(UnityEngine.Random.Range(0, 2));

            if (!isSpawn)
                return false;

            MinedSo spawnMined = _settings.Mined.GetRandom();
            mined = _pools[spawnMined.ID].Pool.Spawn(postion);

            return true;
        }

        [Serializable]
        public class Settings : IMinedMapper
        {
            public List<MinedSo> Mined;

            public IEnumerable<MinedMappingDto> GetParticleMapping() =>
                Mined.Select(item => new MinedMappingDto(item.ID, item));
        }

        public class MinedEntry
        {
            public MinedMono.Pool Pool;
            public MinedSo Def;

            public MinedEntry(MinedSo def, MinedMono.Pool pool)
            {
                Pool = pool;
                Def = def;
            }
        }
    }
}