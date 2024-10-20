using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

namespace Data
{
    [Serializable]
    public class AbilityMeta : BaseMeta
    {
        [Header("- 정적 데이터")]

        [Header("- 능력 이름")]
        [SerializeField] protected string abilityName;

        [Header("- 능력 설명")]
        [SerializeField] protected string abilityInfo;

        [Header("- UI Ability 이미지")]
        [SerializeField] protected Sprite abilityImage;

        [Header("- 능력범위")]
        [SerializeField] protected Range range;

        public string GetAbilityName() => abilityName;
        public string GetAbilityInfo() => abilityInfo;
        public Sprite GetAbilityImage() => abilityImage;
        public Range GetRange() => range;
    }
}
