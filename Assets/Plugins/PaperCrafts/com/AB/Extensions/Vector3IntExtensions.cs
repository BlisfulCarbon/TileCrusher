using UnityEngine;

namespace Plugins.PaperCrafts.com.AB.Extensions
{
    public static class Vector3IntExtensions
    {
        public  static Vector2Int ToVector2Int(this Vector3Int source) => 
            new(source.x, source.y);
    }
}