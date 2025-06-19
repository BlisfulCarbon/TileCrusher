using UnityEngine;

namespace Plugins.PaperCrafts.com.AB.Extensions
{
    public static class MonoBehaviourExtensions
    {
        public static void SetPosition(this MonoBehaviour source, Vector3 position) => 
            source.transform.position = position;
    }
}