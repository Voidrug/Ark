using Data;
using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static Spine.Unity.Examples.SpineboyFootplanter;

namespace UI
{
    [Serializable]
    public class SelectPortraitUI : UIBase
    {
        [Header("- UI View 데이터")]
        [SerializeField] private Button button;
        [SerializeField] private Image back;
        [SerializeField] private Image portrait;
        [SerializeField] private Image middle;
        [SerializeField] private Image up;
        [SerializeField] private Image attribute;
        [SerializeField] private Image tier;
        [SerializeField] private Image front;
        [SerializeField] private Image eliteBack;
        [SerializeField] private Image elite;
        [SerializeField] private Text level;
        [SerializeField] private Image exp;
        [SerializeField] private Image skill;
        [SerializeField] private Text charname;
        [SerializeField] private Button border;

        [Header("- UI Model 데이터")]
        [SerializeField] private CharUIMeta charUIMeta;

        [Header("- UI Field 데이터")]
        [SerializeField] private int portraitIndex = -1;

        public void SetPortraitIndex(int index) => portraitIndex = index;
        public int GetPortraitIndex() => portraitIndex;

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
            //선택됨 테두리 활성화 여부
            SelectUI selectUI = (SelectUI)UIManager.Inst().GetUI(UIEnum.Select);

            if (selectUI.GetSelectedSelectPortraitUI() == this)
            {
                border.gameObject.SetActive(true);
            }
            else
            {
                border.gameObject.SetActive(false);
            }

            //UI정보 업데이트
            PlayerManager playerManager = PlayerManager.Inst();
            UnitRoot unitRoot = playerManager.GetUnitRoots()[portraitIndex];
            Unit unit = unitRoot?.GetUnit();

            UnitMeta unitMeta = unit?.GetUnitMeta();
            Hero hero = unit?.GetStatus<Hero>();
            HeroData heroData = hero?.GetData();
            HeroMeta heroMeta = hero?.GetHeroMeta();

            if (heroMeta)
            {
                int tierNum = (int)heroMeta.GetCharTier();
                int attributeNum = (int)unitMeta.GetAttribute();
                int eliteNum = 0;

                if (back)
                {
                    back.sprite = charUIMeta.GetBackImage(tierNum);
                }
                if (portrait)
                {
                    portrait.sprite = heroMeta.GetPortraitImage();
                }
                if (middle)
                {
                    middle.sprite = charUIMeta.GetMiddleImage(tierNum);
                }
                if (up)
                {
                    up.sprite = charUIMeta.GetUpImage(tierNum);
                }
                if (attribute)
                {
                    attribute.sprite = charUIMeta.GetAttributeImage(attributeNum);
                }
                if (tier)
                {
                    tier.sprite = charUIMeta.GetTierImage(tierNum);
                    tier.SetNativeSize();
                }
                if (front)
                {
                    front.sprite = charUIMeta.GetFrontImage(tierNum);
                }
                if (elite && eliteBack)
                {
                    elite.sprite = charUIMeta.GetEliteImage(eliteNum);

                    elite.gameObject.SetActive(eliteNum != 0);
                    eliteBack.gameObject.SetActive(eliteNum != 0);
                }
                if (level)
                {
                    level.text = heroData.GetLevel().ToString();
                }
                if (exp)
                {
                    exp.fillAmount = (float)heroData.GetExp() / (float)heroMeta.GetHeroExtraStatDatas()[heroData.GetLevel()].GetNeedExp();
                }
                if (charname)
                {
                    charname.text = heroMeta.GetCharName();
                }
                if (skill)
                {
                    skill.sprite = Resources.Load<Sprite>("Texture/Squad/CharSkill"); //기본이미지

                    SelectJson selectJson = playerManager.GetSelectJson();
                    int abilityIndex = selectJson.GetSelectTempNum(portraitIndex); //예외발생


                    //해당 인덱스를 이용해 능력데이터에 접근해서 이미지 설정
                    List<Ability> abilities = unit.GetAbilityList();
                    AbilityMeta abilityMeta = (AbilityMeta)abilities.ElementAtOrDefault(abilityIndex)?.GetMeta();

                    if (abilityMeta)
                    {
                        skill.sprite = abilityMeta.GetAbilityImage();
                    }
                }
            }
        }

        public void ClickPortraitButton()
        {
            //이미 선택이 되어있을때 활성화 여부 선택 
            SelectUI selectUI = (SelectUI)UIManager.Inst().GetUI(UIEnum.Select);
            SelectJson selectJson = PlayerManager.Inst().GetSelectJson();

            if (selectUI)
            {
                if (border.gameObject.activeSelf)
                {
                    selectUI.SetSelectedSelectPortraitIndex(-1);
                    selectUI.SetSelectedSelectAbilityIndex(selectJson.GetSelectTempNum(-1));
                    selectUI.UpdateDataAll();
                }
                else
                {
                    selectUI.SetSelectedSelectPortraitIndex(portraitIndex);
                    selectUI.SetSelectedSelectAbilityIndex(selectJson.GetSelectTempNum(portraitIndex));
                    selectUI.UpdateDataAll();
                }                
            }
        }

    }
}
