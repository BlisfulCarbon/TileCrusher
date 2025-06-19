using System.Collections.Generic;
using UnityEngine;

namespace Plugins.PaperCrafts.com.AB.Extensions
{
    public static class ListExtensions
    {
        public static T GetRandom<T>(this IList<T> source)
        {
            if (source.Count == 0)
                return default;

            return source[Random.Range(0, source.Count)];
        }
    }
}