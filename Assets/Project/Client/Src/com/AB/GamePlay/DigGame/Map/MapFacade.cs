using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using Project.Client.Src.com.AB.GamePlay.DigGame.Map.Filling;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Tilemaps;
using ZLinq;

namespace Project.Client.Src.com.AB.GamePlay.DigGame.Map
{
    [Serializable]
    public class MapFacade
    {
        readonly Dictionary<string, LayerFacade> _layers = new Dictionary<string, LayerFacade>();

        public IEnumerable<LayerFacade> Layers
            => _layers.Values;

        public void AddNewLayers(Tilemap layerPrefab, List<MapFillingLayerSo> layersDef, Transform container,
            Vector3 offset)
        {
            foreach (var layerElement in layersDef.Select((item, index) => (index, item)))
            {
                LayerFacade layer = new LayerFacade(
                    layerElement.item, 
                    layerPrefab, container, 
                    layerElement.index * offset);

                layer.SetOrder(-_layers.Count);
                _layers.Add(layer.GetId, layer);
            }
        }

        public void SetTileAllLayers(Vector3Int position, int topologyId = 0)
        {
            foreach (LayerFacade layer in _layers.Values) 
                layer.SetTile(position, topologyId);
        }

        public void PrintTiles()
        {
            foreach (var layer in Layers)
            {
                foreach (var position in layer.Layer.cellBounds.allPositionsWithin)
                {
                    if (layer.Layer.HasTile(position))
                    {
                        Debug.Log($"MapFacade::layer:{layer.GetId}; position: {position}");
                    }
                }
            }
        }
    }
}