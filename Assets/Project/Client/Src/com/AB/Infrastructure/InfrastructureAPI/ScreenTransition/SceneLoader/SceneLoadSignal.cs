namespace Project.Client.Src.com.AB.Infrastructure.InfrastructureAPI.ScreenTransition.SceneLoader
{
    public class SceneLoadSignal
    {
        public string SceneName { get; }
        
        public bool AllowSceneActivation;
        public object Payload { get; }

        public SceneLoadSignal(
            string sceneName, 
            bool allowSceneActivation = false, 
            object payload = null )
        {
            SceneName = sceneName;
            Payload = payload;
            AllowSceneActivation = allowSceneActivation;
        }
    }
}