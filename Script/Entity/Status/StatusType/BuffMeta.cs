using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    [Serializable]
    [CreateAssetMenu(fileName = "BuffData", menuName = "Base/Status/BuffMeta")]
    public class BuffMeta : StatusMeta
    {
        [Header("- 정적 데이터")]

        [Header("- 동적 데이터")]
        [SerializeField] private BuffData buffData;

        public BuffData GetBuffData() => buffData;
    }
}
