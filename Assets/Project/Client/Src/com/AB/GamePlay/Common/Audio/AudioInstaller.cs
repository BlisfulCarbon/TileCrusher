using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Project.Client.Src.com.AB.GamePlay.Common.Audio
{
    public class AudioInstaller : MonoInstaller
    {
        [InlineEditor] [SerializeField] ScriptableObject[] _mappers;
        IAudioMapper[] Mappers => _mappers.Select(item => (IAudioMapper)item).ToArray();

        [Header("Settings")] 
        [SerializeField] AudioService.Setting AudioSettings;
        [SerializeField] AudioPlayerService.Settings PlayerSettings;

        public override void InstallBindings()
        {
            AudioSettings.Mappers = Mappers;
            Container.BindInstance(transform).WithId(AudioConst.AUDIO_CONTAINER_KEY);

            Container.Bind<AudioPlayerService>().AsSingle();
            Container.BindInstance(PlayerSettings);
            
            Container.Bind<AudioService>().AsSingle();
            Container.BindInstances(AudioSettings);
        }
    }
}