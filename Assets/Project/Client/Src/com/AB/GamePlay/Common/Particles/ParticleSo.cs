using Project.Client.Src.com.AB.GamePlay.Common.Const;
using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.Common.Particles
{
    [CreateAssetMenu(
        fileName = "$Name$ParticleDef",
        menuName = AssetsConst.ASSET_MENU_PARTICLE_PATH)]
    public class ParticleSo : ScriptableObject
    {
        public ParticleMono ParticlePrefab;
        public float LifeTime;
        public int MaxInstances = 10;
    }
}