using Project.Client.Src.com.AB.GamePlay.Common.Const;
using Project.Client.Src.com.AB.GamePlay.Common.Defs;
using UnityEngine;

namespace Project.Client.Src.com.AB.GamePlay.Common.Particles
{
    [CreateAssetMenu(
        fileName = "DigGame$Name$ParticleDef",
        menuName = AssetsConst.ASSET_MENU_PARTICLE_PATH)]
    public class ParticleSo : DefsBase
    {
        public ParticleMono ParticlePrefab;
        public float LifeTime;
        public int MaxInstances = 10;
        string _id;
        string _id1;
    }

}