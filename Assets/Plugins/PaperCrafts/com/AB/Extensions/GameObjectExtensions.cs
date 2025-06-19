using UnityEngine;

namespace Plugins.PaperCrafts.com.AB.Extensions
{
    public static class GameObjectExtensions
    {
        public static GameObject WithName(this GameObject source, string name)
        {
            source.name = name;
            return source;
        }
    }
}