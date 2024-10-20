using Data;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class SquadPortraitUI : UIBase
    {
        [Header("- UI View 데이터")]
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
        [SerializeField] private Image border;

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
            //캐릭터가 데이터 범위 밖일때
            PlayerManager playerManager = PlayerManager.Inst();
            SquadJson squadJson = playerManager.GetSquadJson();
            SelectJson selectJson = playerManager.GetSelectJson();
            List<UnitRoot> unitRoots = playerManager.GetUnitRoots();
            int charIndex = squadJson.GetSquadCharNum(portraitIndex);
            int abilityIndex = selectJson.GetSelectAbilityNum(charIndex);

            //스쿼드의 영웅데이터가 사용불가능하다면 해당 데이터 초기화
            if (charIndex < 0  || charIndex >= unitRoots.Count)
            {
                gameObject.SetActive(false);
                return;
            }
            else
            {
                gameObject.SetActive(true);
            }

            //UI정보 업데이트
            UnitRoot root = unitRoots[charIndex];
            Unit unit = root?.GetUnit();
            List<Ability> abilities = unit?.GetAbilityList();
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
                    if (abilityIndex < 0 || abilityIndex >= abilities.Count)
                    {
                        skill.sprite = Resources.Load<Sprite>("Texture/Squad/CharSkill");
                    }
                    else
                    {
                        skill.sprite = ((AbilityMeta)abilities[abilityIndex].GetMeta()).GetAbilityImage();
                    }
                }
            }
        }
        public void OpenUISelect()
        {
            SquadUI squadUI = (SquadUI)UIManager.Inst().GetUI(UIEnum.Squad);
            SelectUI selectUI = (SelectUI)UIManager.Inst().GetUI(UIEnum.Select);

            if (squadUI)
            {
                squadUI.CloseUI();
                squadUI.SetSelectedPoratraitIndex(portraitIndex);
            }


            if (selectUI)
            {
                selectUI.OpenUI();
            }
        }
    }
}
