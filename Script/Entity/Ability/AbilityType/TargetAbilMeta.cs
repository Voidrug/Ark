using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    [Serializable]
    [CreateAssetMenu(fileName = "TargetAbilData", menuName = "Base/Ability/TargetAbilMeta")]
    public class TargetAbilMeta : AbilityMeta
    {
        [Header("- 동적 데이터")]
        [SerializeField] private TargetAbilData targetAbilData;

        public TargetAbilData GetTargetAbil() => targetAbilData;
    }
}
