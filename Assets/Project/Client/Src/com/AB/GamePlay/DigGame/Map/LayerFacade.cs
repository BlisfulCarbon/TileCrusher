using System;
using Project.Client.Src.com.AB.GamePlay.DigGame.Map.Filling;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Map
{
    public class LayerFacade
    {
        public const int LAST_TOPOLOGY_ID = -1;
        
        public readonly Tilemap Layer;
        public readonly TilemapRenderer LayerRenderer;
        public readonly MapFillingLayerSo Def;

        public LayerFacade(MapFillingLayerSo def, Tilemap prefab, Transform container, Vector3 offset)
        {
            Def = def;

            if (string.IsNullOrEmpty(Def.LayerID))
                Def.LayerID = Guid.NewGuid().ToString();

            Layer = Object.Instantiate(prefab, container, true);
            LayerRenderer = Layer.GetComponent<TilemapRenderer>();
            
            Layer.transform.position = Vector3.zero + offset;
            Layer.name = Def.LayerID;
        }

        public string GetId => Def.LayerID;

        public void SetOrder(int index) => 
            this.LayerRenderer.sortingOrder = index;

        public void SetTile(Vector3Int position, int topologyId = 0)
        {
            TileBase tile = Def.TopologyTiles[topologyId];
            Layer.SetTile(position, tile);
        }
    }
}