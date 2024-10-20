using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static Spine.Unity.Examples.SpineboyFootplanter;

namespace Data
{
    [Serializable]
    public class StatusBar : Actor
    {
        [Header("- 상태창 데이터")]
        [SerializeField] private Slider healthBar;
        [SerializeField] private Slider manaBar;

        [Header("- 원본 데이터")]
        [SerializeField] private StatusBarMeta statusBarMeta;

        [Header("- 인게임 데이터")]
        [SerializeField, ReadOnly] private StatusBarData StatusBarData;

        public Slider GetHealthBar() => healthBar;
        public Slider GetManaBar() => manaBar;
        public override BaseMeta GetMeta() => statusBarMeta;
        public override ActorMeta GetActorMeta() => statusBarMeta;
        public override void UpdateData()
        {
        }
        public override void PreDestroy()
        {
            //하위 오브젝트 해제
            for (int i = 0; i < actors.Count; i++)
            {
                actors[i].PreDestroy();
            }

            DestroyManager.Inst().Destroy(this);
        }

        public override void LastDestroy()
        {
            if (gameObject.activeSelf == false)
                return;

            transform.parent = null;
            actors.Clear();
            PoolManager.Inst().ReleaseObj(gameObject, statusBarMeta);
        }

    }
}
