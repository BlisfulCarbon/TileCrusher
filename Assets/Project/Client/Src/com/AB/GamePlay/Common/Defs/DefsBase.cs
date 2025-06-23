using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.Common.Defs
{
    public abstract class DefsBase : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
    }
}