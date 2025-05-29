namespace Project.Client.Src.com.AB.Infrastructure.InfrastructureAPI.ScreenTransition.Fader
{
    public class FaderOutSignal
    {
        public float Duration;

        public FaderOutSignal(float duration = -1f) => 
            Duration = duration;
    }
}