using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Data
{
    [Serializable]
    [CreateAssetMenu(fileName = "StatusBarData", menuName = "Base/Actor/StatusBarMeta")]
    public class StatusBarMeta : ActorMeta
    {
        [Header("- 동적 데이터")]
        [SerializeField] private StatusBarData statusBarData;

        public StatusBarData GetStatusBarData() => statusBarData;

        public void UpdateStatusBar(Root root, Actor actor) //root = 디폴트 출처 (아직 미사용), actor = 실행된 actor
        {
            //체력바 업데이트
            Slider healthBar = ((StatusBar)actor).GetHealthBar();
            Slider manaBar = ((StatusBar)actor).GetManaBar();
            UnitData unitData = ((UnitRoot)actor.GetRoot())?.GetUnit()?.GetData();

            if (unitData != null)
            {
                if(healthBar)
                {
                    healthBar.value = unitData.GetHp() / unitData.GetHpMax() * 100;
                }

                if(manaBar)
                {
                    healthBar.value = unitData.GetMp() / unitData.GetMpMax() * 100;
                }
            }
        }
    }
}
