using System;
using System.Collections.Generic;
using Plugins.PaperCrafts.com.AB.Extensions;
using Project.Client.Src.com.AB.GamePlay.DigGame.Map.Filling;
using Project.Client.Src.com.AB.GamePlay.DigGame.Mined;
using Project.Client.Src.com.AB.GamePlay.DigGame.React;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Map
{
    public class MapLayerFacade
    {
        readonly string ID;
        readonly Tilemap _tilemap;
        readonly TilemapRenderer _layerRenderer;
        readonly MapFillingLayerSo _def;
        readonly Dictionary<Vector2Int, int> _interactionsCount = new();

        public MapLayerFacade(
            string id,
            MapFillingLayerSo def,
            Tilemap prefab,
            Transform container,
            Vector3 offset)
        {
            ID = id;
            _def = def;

            if (string.IsNullOrEmpty(_def.LayerID))
                _def.LayerID = Guid.NewGuid().ToString();

            _tilemap = Object.Instantiate(prefab, container, true);
            _layerRenderer = _tilemap.GetComponent<TilemapRenderer>();

            _tilemap.transform.position = Vector3.zero + offset;
            _tilemap.name = _def.LayerID;
        }

        public bool HasTile(Vector3Int cellPosition) => 
            _tilemap.HasTile(cellPosition);

        public void Break(Vector2Int cellPosition, out bool broken)
        {
            int countInteractionsWithCell = UpdateInteractions(cellPosition);
            broken = countInteractionsWithCell >= _def.TopologyTiles.Count;
            TileBase tile = broken ? null : _def.TopologyTiles[countInteractionsWithCell];

            _tilemap.SetTile(cellPosition.ToVector3Int(), tile);
        }

        int UpdateInteractions(Vector2Int cellPosition)
        {
            int countInteractionsWithCell = 0;

            _interactionsCount.TryGetValue(cellPosition, out countInteractionsWithCell);

            countInteractionsWithCell++;
            _interactionsCount[cellPosition] = countInteractionsWithCell;
            return countInteractionsWithCell;
        }

        public Tilemap GetTilemap() => _tilemap;

        public void SetOrder(int index) =>
            this._layerRenderer.sortingOrder = index;

        public void SetTile(Vector3Int position, int topologyId = 0)
        {
            TileBase tile = _def.TopologyTiles[topologyId];
            _tilemap.SetTile(position, tile);
        }

        public ReactList GetActions() => 
            _def.Actions;
    }
}