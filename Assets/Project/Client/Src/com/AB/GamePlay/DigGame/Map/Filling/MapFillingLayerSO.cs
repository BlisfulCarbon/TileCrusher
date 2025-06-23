using System.Collections.Generic;
using Project.Client.Src.com.AB.GamePlay.DigGame.React;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Map.Filling
{
    [CreateAssetMenu(
        fileName = "DigGameFilling$Name$MapDef",
        menuName = DigGameConst.ASSET_MENU_DIGGAME_PATH + "MapFillingDef")]
    public class MapFillingLayerSo : ScriptableObject
    {
        public string LayerID;
        public List<TileBase> TopologyTiles;

        public ReactList Actions;
    }
}