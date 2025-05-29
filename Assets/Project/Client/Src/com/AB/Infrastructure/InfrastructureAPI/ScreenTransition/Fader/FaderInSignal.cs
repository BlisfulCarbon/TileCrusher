namespace Project.Client.Src.com.AB.Infrastructure.InfrastructureAPI.ScreenTransition.Fader
{
    public class FaderInSignal
    {
        public float Duration { get; set; }

        public FaderInSignal(float duration = -1f) => 
            Duration = duration;
    }
}