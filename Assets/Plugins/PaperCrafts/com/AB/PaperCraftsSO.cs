using Plugins.PaperCrafts.com.AB.Editor;
using UnityEngine;

namespace Plugins.PaperCrafts.com.AB
{
    [CreateAssetMenu(
        fileName = "PaperCraftDef",
        menuName = "Plugins/PaperCrafts/PaperCraftDef")]
    public class PaperCraftsSO : ScriptableObject
    {
        public const string DEF_PATH = "Assets/Plugins/PaperCrafts/com/AB/Defs/PaperCraftDef.asset";

        public SceneReference BootScene;
    }
}