using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    [Serializable]
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Base/WeaponMeta")]
    public class WeaponMeta : BaseMeta
    {
        [Header("- 정적 데이터")]

        [Header("- 필터")]
        [SerializeField] private Filter filter;

        [Header("- 대상")]
        [SerializeField] private Aliance aliance;

        [Header("- 무기범위")]
        [SerializeField] private Range range = new Range();

        [Header("- 동적 데이터")]
        [SerializeField] private WeaponData weaponData;

        public Filter GetFilter() => filter;
        public Aliance GetAliance() => aliance;
        public Range GetRange() => range;
        public WeaponData GetWeaponData() => weaponData;
    }
}
