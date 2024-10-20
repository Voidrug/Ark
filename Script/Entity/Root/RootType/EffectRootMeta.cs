using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    [Serializable]
    [CreateAssetMenu(fileName = "EffectRootData", menuName = "Base/Root/EffectRootMeta")]

    public class EffectRootMeta : RootMeta
    {
        [Header("- 동적 데이터")]
        [SerializeField] private EffectCreatorData effectCreatorData;
    }

}
