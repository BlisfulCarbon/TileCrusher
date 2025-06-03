using System.Collections.Generic;
using Project.Client.Src.com.AB.GamePlay.Common.Const;
using Project.Client.Src.com.AB.GamePlay.Common.Particle;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Map.Filling
{
    [CreateAssetMenu(
        fileName = "$Name$MapFillingLayerDef", 
        menuName = EditorConst.ASSET_MENU_DIG_GAME + "MapFillingDef")]
    public class MapFillingLayerSo : ScriptableObject
    {
        public string LayerID;

        public List<TileBase> TopologyTiles;
        
        public ParticleSo BreakParticle;
        public ParticleSo BrokenParticle;
    }
}