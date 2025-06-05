using Project.Client.Src.com.AB.GamePlay.Common.Const;
using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.Common.Particle
{
    [CreateAssetMenu(fileName = "$Name$ParticleDef", menuName = EditorConst.ASSET_MENU_COMMON_PATH + "ParticleDef")]
    public class ParticleSo : ScriptableObject
    {
        public float LifeTime;
        public ParticleSystem ParticlePrefab;
    }
}