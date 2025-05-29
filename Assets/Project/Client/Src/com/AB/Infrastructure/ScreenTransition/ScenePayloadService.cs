namespace Project.Client.Src.com.AB.Infrastructure.ScreenTransition
{
    public class ScenePayloadService
    {
        object _payload;

        public void Store(object payload) => 
            _payload = payload;

        public T Get<T>()
        {
            var value = (T)_payload;
            _payload = null;  
            return value;
        }
    }
}