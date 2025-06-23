namespace Project.Client.Src.com.AB.GamePlay.DigGame.Mined
{
    public class MinedEntry
    {
        public readonly MinedMono.Pool Pool;
        public readonly MinedSo Def;

        public MinedEntry(MinedSo def, MinedMono.Pool pool)
        {
            Pool = pool;
            Def = def;
        }
    }
}