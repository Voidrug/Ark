using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    [Serializable]
    public abstract class Status : Base
    {
        [Header("- 자식 데이터")]
        [SerializeField] private Effect initialPrefab;
        [SerializeField] private Effect periodPrefab;
        [SerializeField] private Effect finalPrefab;

    }
}
