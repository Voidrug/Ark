using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    [Serializable]
    [CreateAssetMenu(fileName = "MoveData", menuName = "Base/Ability/MoveMeta")]
    public class MoveMeta : AbilityMeta
    {
        [Header("- 동적 데이터")]
        [SerializeField] private MoveData MoveData;

        public MoveData GetMove() => MoveData;
    }
}
