using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    [Serializable]
    [CreateAssetMenu(fileName = "DamageData", menuName = "Base/Effect/DamageMeta")]

    public class DamageMeta : EffectMeta
    {
        [Header("- 정적 데이터")]

        [Header("- 방사피해 지점")]
        [SerializeField] private Location launchLocation = Location.Caster; //AOE의 방향이 해당 개체를 기준으로 정해집니다.
        [SerializeField] private bool isLaunchLand = false;
        [SerializeField] private Location impactLocation = Location.Target; //AOE가 해당 지점에서 발생합니다.
        [SerializeField] private bool isImpactLand = false;

        [Header("- 플래그")]
        [SerializeField] private DamageFlag flag;

        [Header("- 피해누산기")]
        [SerializeField] private List<Accumulator> accumulators = new List<Accumulator>();

        [Header("- 피해범위")]
        [SerializeField] private Range range = new Range();

        [Header("- 동적 데이터")]
        [SerializeField] private DamageData damageData;

        public Location GetLaunchLocation() => launchLocation;
        public bool IsLaunchLand() => isLaunchLand;
        public Location GetImpactLocation() => impactLocation;
        public bool IsImpactLand() => isImpactLand;
        public DamageFlag GetFlag() => flag;
        public List<Accumulator> GetAccumulators() => accumulators;
        public Range GetRange() => range;
        public DamageData GetDamageData() => damageData;


    }

}