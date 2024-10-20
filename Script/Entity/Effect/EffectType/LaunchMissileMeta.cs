using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;

namespace Data
{
    [Serializable]
    [CreateAssetMenu(fileName = "LaunchMissileData", menuName = "Base/Effect/LaunchMissileMeta")]

    public class LaunchMissileMeta : EffectMeta
    {
        [Header("- 정적 데이터")]

        [Header("- 미사일 지점")]
        [SerializeField] private Location launchLocation = Location.Caster;
        [SerializeField] private bool isLaunchLand = false;
        [SerializeField] private Location impactLocation = Location.Target;
        [SerializeField] private bool isImpactLand = false;

        [Header("- 플래그")]
        [SerializeField] private LMFlag flag = LMFlag.CasterDir; 

        [Header("- 동적 데이터")]
        [SerializeField] private LaunchMissileData launchMissileData;


        public Location GetLaunchLocation() => launchLocation;
        public bool IsLaunchLand() => isLaunchLand;
        public Location GetImpactLocation() => impactLocation;
        public bool IsImpactLand() => isImpactLand;
        public LMFlag GetFlag() => flag;

    }

}