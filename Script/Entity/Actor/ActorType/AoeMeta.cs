using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    [Serializable]
    [CreateAssetMenu(fileName = "AoeData", menuName = "Base/Actor/AoeMeta")]
    public class AoeMeta : ActorMeta
    {
        [Header("- 동적 데이터")]
        [SerializeField] private AoeData aoeData;

        public AoeData GetAoeData() => aoeData;

    }
}
