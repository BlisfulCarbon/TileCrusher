using System;
using System.Collections.Generic;
using Project.Client.Src.com.AB.GamePlay.DigGame.Map.Filling;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Map
{
    public class LayerFacade
    {
        public const int LAST_TOPOLOGY_ID = -1;
        
        public readonly Tilemap Tilemap;
        public readonly Dictionary<Vector2Int, int> InteractionsMap = new ();
        public readonly TilemapRenderer LayerRenderer;
        public readonly MapFillingLayerSo Def;

        public LayerFacade(MapFillingLayerSo def, Tilemap prefab, Transform container, Vector3 offset)
        {
            Def = def;

            if (string.IsNullOrEmpty(Def.LayerID))
                Def.LayerID = Guid.NewGuid().ToString();

            Tilemap = Object.Instantiate(prefab, container, true);
            LayerRenderer = Tilemap.GetComponent<TilemapRenderer>();
            
            Tilemap.transform.position = Vector3.zero + offset;
            Tilemap.name = Def.LayerID;
        }

        public string GetId => Def.LayerID;

        public void SetOrder(int index) => 
            this.LayerRenderer.sortingOrder = index;

        public void SetTile(Vector3Int position, int topologyId = 0)
        {
            TileBase tile = Def.TopologyTiles[topologyId];
            Tilemap.SetTile(position, tile);
        }
    }
}