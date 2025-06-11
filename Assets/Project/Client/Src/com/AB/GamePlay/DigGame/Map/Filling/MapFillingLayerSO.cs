using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Project.Client.Src.com.AB.GamePlay.Common.Const;
using Project.Client.Src.com.AB.GamePlay.Common.Particles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Map.Filling
{
    [CreateAssetMenu(
        fileName = "$Name$MapFillingLayerDef", 
        menuName = AssetsConst.ASSET_MENU_DIG_GAME + "MapFillingDef")]
    public class MapFillingLayerSo : ScriptableObject
    {
        public string LayerID;
        public List<TileBase> TopologyTiles;
        
        public ParticleSo BreakParticle;
        public ParticleSo BrokenParticle;

        const string PARTIKLE_BREAK_SUFIX_KEY = "_break";
        const string PARTIKLE_BROKEN_SUFIX_KEY = "_broken";

        public string GetBreakKey() => 
            LayerID + PARTIKLE_BREAK_SUFIX_KEY;
        
        public string GetBrokenKey() => 
            LayerID + PARTIKLE_BROKEN_SUFIX_KEY;
        
        public List<ParticleMappingItem> GetParticleMapping()
        {
            IEnumerable<ParticleMappingItem> items = new List<ParticleMappingItem>();

            return new List<ParticleMappingItem>
            {
                new ParticleMappingItem(GetBreakKey(), BreakParticle),
                new ParticleMappingItem(GetBrokenKey(), BrokenParticle),
            };
        }
    }
}