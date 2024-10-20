using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    [Serializable]
    [CreateAssetMenu(fileName = "UnitRootData", menuName = "Base/Root/UnitRootMeta")]

    public class UnitRootMeta : RootMeta
    {
        [Header("- 동적 데이터")]
        [SerializeField] private UnitRootData unitRootData;
    }
}
