namespace Project.Client.Src.com.AB.GamePlay.DigGame.Mined
{
    public class MinedMappingDto
    {
        public readonly string Key;
        public readonly MinedSo Def;
        
        public MinedMappingDto(string key, MinedSo def)
        {
            Key = key;
            Def = def;
        }
    }
}