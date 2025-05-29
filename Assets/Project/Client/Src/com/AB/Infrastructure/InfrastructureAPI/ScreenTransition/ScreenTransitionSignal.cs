namespace Project.Client.Src.com.AB.Infrastructure.InfrastructureAPI.ScreenTransition
{
    public class ScreenTransitionSignal
    {
        public string SceneName { get; }

        public ScreenTransitionSignal(string sceneName) => 
            SceneName = sceneName;
    }
}