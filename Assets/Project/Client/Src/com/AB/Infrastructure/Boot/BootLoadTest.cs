using Cysharp.Threading.Tasks;
using Project.Client.Src.com.AB.Infrastructure.InfrastructureAPI.Boot;
using UnityEngine;
using Zenject;

namespace Project.Client.Src.com.AB.Infrastructure.Boot
{
    public class BootLoadTest : IInitializable, IBootLoad
    {
        bool _afterInitializeLeft3Seconds;
        
        public UniTask LoadComplete => UniTask.WaitUntil(() => _afterInitializeLeft3Seconds);

        public void Initialize()
        {
            MakeLoaded();
        }

        async void MakeLoaded()
        {
            // await UniTask.WaitForSeconds(3f);

            Debug.Log($"Boot load test loaded");
            
            _afterInitializeLeft3Seconds = true;
        }
    }
}