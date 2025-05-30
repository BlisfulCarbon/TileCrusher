using Cysharp.Threading.Tasks;

namespace Project.Client.Src.com.AB.Infrastructure.InfrastructureAPI.Boot
{
    public interface IBootLoad
    {
        public UniTask LoadComplete { get; }
    }
}