using System;
using System.Collections.Generic;
using System.Linq;
using Project.Client.Src.com.AB.GamePlay.DigGame.Map.Filling;
using Project.Client.Src.com.AB.GamePlay.DigGame.Mined;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Map
{
    public class MapLayerFacade
    {
        public readonly Tilemap Tilemap;
        public readonly TilemapRenderer LayerRenderer;
        public readonly MapFillingLayerSo Def;
        public readonly Dictionary<Vector2Int, MapTileState> TileStates = new();
        public readonly Dictionary<Vector2Int, int> InteractionsMap = new();

        public MapLayerFacade(MapFillingLayerSo def, Tilemap prefab, Transform container, Vector3 offset)
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

        public bool IsMinedAttach(Vector2Int cellPosition, out MapTileState state)
        {
            state = null;

            if (!TileStates.TryGetValue(cellPosition, out state))
                return false;

            return state.AttachedMined != null;
        }

        public void AddMined(Vector2Int position, MinedMono mined)
        {
            MapTileState state = GetState(position);
            state.AttachedMined = mined;
        }

        MapTileState GetState(Vector2Int position)
        {
            MapTileState state;
            if (!TileStates.TryGetValue(position, out state))
                state = new MapTileState();

            TileStates[position] = state;
            return state;
        }
    }

    public class MapTileState
    {
        public int InteractionsCount;
        public MinedMono AttachedMined;
    }
}