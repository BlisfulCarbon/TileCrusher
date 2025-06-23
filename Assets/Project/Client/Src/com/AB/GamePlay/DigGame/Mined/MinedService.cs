using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Plugins.PaperCrafts.com.AB.Extensions;
using Project.Client.Src.com.AB.GamePlay.Common.Audio;
using Project.Client.Src.com.AB.GamePlay.Common.Particles;
using Project.Client.Src.com.AB.GamePlay.DigGame.Map;
using Project.Client.Src.com.AB.GamePlay.DigGame.React;
using UniRx;
using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Mined
{
    public class MinedService : IMinedService
    {
        readonly Settings _settings;
        readonly MapService _map;
        readonly IReactService _react;
        readonly Dictionary<string, MinedEntry> _pools;
        readonly Dictionary<Vector3Int, MinedCell> _cells = new();

        public MinedService(Settings settings, MinedPoolRef pools, IReactService react)
        {
            _settings = settings;
            _react = react;
            
            _pools = pools.Items.ToDictionary(item => item.Key.ID, item =>
                new MinedEntry(item.Key, item.Value));
        }

        bool HasMined(Vector3Int cellPosition) =>
            _cells.ContainsKey(cellPosition);

        void DespawnCell(Vector3Int cellPosition)
        {
            MinedCell cell = _cells[cellPosition];
            _cells.Remove(cellPosition);
            _pools[cell.ID].Pool.Despawn(cell.MinedInstance);
        }

        public bool Break(Vector3Int cellPosition, out IReact react)
        {
            react = null;

            bool hasMined = HasMined(cellPosition);
            if (!hasMined)
                return false;

            MinedCell minedCell = _cells[cellPosition];
            minedCell.HitCount++;

            bool broken = minedCell.HitCount >= _pools[minedCell.ID].Def.BreakCountMax;

            if (broken) //TODO: Mined break logic wait animation despawn
            {
                minedCell.MinedInstance.Broken();
                Observable.Timer(TimeSpan.FromSeconds(0.12f)).Subscribe(_ => DespawnCell(cellPosition));
            }
            else
            {
                minedCell.MinedInstance.Break();
            }

            react = _react.GetReact(_pools[minedCell.ID].Def.Actions, broken);
            return true;
        }

        public bool TrySpawn(Vector3Int cellPosition, Vector2 worldCenter)
        {
            bool isSpawn = Convert.ToBoolean(UnityEngine.Random.Range(0, 2));

            if (!isSpawn)
                return false;

            MinedSo spawnMined = _settings.Mined.GetRandom();
            _cells[cellPosition] = new MinedCell(spawnMined.ID,
                _pools[spawnMined.ID].Pool.Spawn(worldCenter));

            return true;
        }

        [Serializable]
        public class Settings : IAudioMapper, IParticleMapper
        {
            public List<MinedSo> Mined;

            public IEnumerable<ParticleDto> GetParticleMapping() =>
                Mined.SelectMany(item => item.Actions.GetParticleMapping());

            public IEnumerable<AudioDto> GetAudiosMapping() =>
                Mined.SelectMany(item => item.Actions.GetAudioMapping());
        }
    }
}