using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    [Serializable]
    [CreateAssetMenu(fileName = "InstantAbilData", menuName = "Base/Ability/InstantAbilMeta")]
    public class InstantAbilMeta : AbilityMeta
    {
        [Header("- 동적 데이터")]
        [SerializeField] private InstantAbilData instantAbilData;

        public InstantAbilData GetInstantAbil() => instantAbilData;
    }
}