using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Client.Src.com.AB.Infrastructure.InfrastructureAPI.Boot;

namespace Project.Client.Src.com.AB.Infrastructure.Boot
{
    public class BootLoadCoordinator
    {
        readonly List<IBootLoad> _loads;
        
        public BootLoadCoordinator(List<IBootLoad> loads) => 
            _loads = loads;

        public async UniTask WhenAllLoad()
        {
            await UniTask.WhenAll( _loads.Select(item => item.LoadComplete));
        }
    }
}