namespace Project.Client.Src.com.AB.GamePlay.DigGame.Mined
{
    public class MinedCell
    {
        public readonly string ID;
        public readonly MinedMono MinedInstance;
        public int HitCount;

        public MinedCell(string id, MinedMono instance)
        {
            ID = id;
            MinedInstance = instance;
        }
    }
}