using Data;
using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class SelectAbilityInfoUI : UIBase
    {
        [Header("- UI View 데이터")]
        [SerializeField] private Image border;
        [SerializeField] private Image skillIcon;
        [SerializeField] private Text skillName;
        [SerializeField] private Image regenAuto;
        [SerializeField] private Image regenManual;
        [SerializeField] private Image startAuto;
        [SerializeField] private Image startManual;
        [SerializeField] private Text info;

        [Header("- UI Field 데이터")]
        [SerializeField] private int abilityIndex = -1;

        public void SetAbilityIndex(int index) => abilityIndex = index;
        public int GetAbilityIndex() => abilityIndex;
        public override void OpenUI()
        {
            Init();

            UpdateDataAll();
        }
        public override void CloseUI()
        {
            gameObject.SetActive(false);
        }
        public override void Init()
        {
            gameObject.SetActive(true);
        }
        public override void UpdateDataAll()
        {
            UpdateData();
        }
        public override void UpdateData()
        {
            SelectUI selectUI = (SelectUI)UIManager.Inst().GetUI(UIEnum.Select);
            SquadUI squadUI = (SquadUI)UIManager.Inst().GetUI(UIEnum.Squad);
            SelectPortraitUI selectPortraitUI = selectUI.GetSelectedSelectPortraitUI();
            SelectAbilityInfoUI selectAbilityInfoUI = selectUI.GetSelectedSelectAbilityUI();

            PlayerManager playerManager = PlayerManager.Inst();
            List<UnitRoot> unitRoots = playerManager.GetUnitRoots();
            Unit unit = selectPortraitUI != null? unitRoots[selectPortraitUI.GetPortraitIndex()].GetUnit() : null;
            AbilityMeta abilityMeta = (AbilityMeta)unit?.SearchSkillAbility(abilityIndex)?.GetMeta();
            
            //데이터 없으면 비활성화
            if(abilityMeta == null)
            {
                gameObject.SetActive(false);
                return;
            }
            else
            {
                gameObject.SetActive(true);
            }

            //테두리 설정 여부
            if (selectAbilityInfoUI == this)
            {
                border.gameObject.SetActive(true);
            }
            else
            {
                border.gameObject.SetActive(false);
            }


            //UI 업데이트
            if (squadUI)
            {
                if (skillIcon && skillName)
                {
                    skillIcon.sprite = abilityMeta.GetAbilityImage();
                    skillName.text = abilityMeta.GetAbilityName();
                }

                if (regenAuto && regenManual)
                {
                    if (regenAuto)
                    {
                        regenAuto.gameObject.SetActive(true);
                        regenManual.gameObject.SetActive(false);
                    }
                    else
                    {
                        regenAuto.gameObject.SetActive(false);
                        regenManual.gameObject.SetActive(true);
                    }
                }

                if (startAuto && startManual)
                {
                    if (startAuto)
                    {
                        startAuto.gameObject.SetActive(true);
                        startManual.gameObject.SetActive(false);
                    }
                    else
                    {
                        startAuto.gameObject.SetActive(false);
                        startManual.gameObject.SetActive(true);
                    }
                }

                if (info)
                {
                    info.text = abilityMeta.GetAbilityInfo();
                }
            }
        }

        public void ClickAbilityButton()
        {
            SquadJson squadJson = PlayerManager.Inst().GetSquadJson();
            SquadUI squadUI = (SquadUI)UIManager.Inst().GetUI(UIEnum.Squad);
            SelectJson selectJson = PlayerManager.Inst().GetSelectJson();
            SelectUI selectUI = (SelectUI)UIManager.Inst().GetUI(UIEnum.Select);            

            if (border.gameObject.activeSelf)
            {
                selectJson.SetSelectTempNum( -1, selectUI.GetSelectedSelectPortraitIndex());

                selectUI.SetSelectedSelectAbilityIndex(-1);

                selectUI.UpdateDataAll();
            }
            else
            {
                selectJson.SetSelectTempNum(abilityIndex, selectUI.GetSelectedSelectPortraitIndex());

                selectUI.SetSelectedSelectAbilityIndex(abilityIndex);

                selectUI.UpdateDataAll();
            }
        }
    }
}
