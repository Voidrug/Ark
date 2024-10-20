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
    [CreateAssetMenu(fileName = "EnumAreaData", menuName = "Base/Effect/EnumAreaMeta")]

    public class EnumAreaMeta : EffectMeta
    {
        [Header("- 정적 데이터")]
        [SerializeField] private EffectMeta effectMeta;

        [Header("- 동적 데이터")]
        [SerializeField] private EnumAreaData enumAreaData;
    }

}
