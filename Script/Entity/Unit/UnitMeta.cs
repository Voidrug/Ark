using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    [Serializable]
    [CreateAssetMenu(fileName = "UnitData", menuName = "Base/Unit/UnitMeta")]    
    public class UnitMeta : BaseMeta
    {
        [Header("- 정적 데이터")]

        [Header("- 유닛 직군")]
        [SerializeField] private Attribute attribute = Attribute.Vanguard;

        [Header("- 동적 데이터")]
        [SerializeField] private UnitData unitData;


        public Attribute GetAttribute() => attribute;
        public UnitData GetUnitData() => unitData;
    }
}
