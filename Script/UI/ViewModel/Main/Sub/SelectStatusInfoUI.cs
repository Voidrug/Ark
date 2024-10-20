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
    public class SelectStatusInfoUI : UIBase
    {
        [Header("- UI View 데이터")]
        [SerializeField] private Image back;
        [SerializeField] private Image infoPanel;
        [SerializeField] private Text charName;
        [SerializeField] private Text levelMax;
        [SerializeField] private Text level;
        [SerializeField] private Image attribute;
        [SerializeField] private Text health;
        [SerializeField] private Text attack;
        [SerializeField] private Text defense;
        [SerializeField] private Text magicResistance;
        [SerializeField] private Text respwan;
        [SerializeField] private Text cost;
        [SerializeField] private Text block;
        [SerializeField] private Text attackSpeed;

        [Header("- UI Model 데이터")]
        [SerializeField] private CharUIMeta charUIMeta;

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
            SquadUI squadUI = (SquadUI)UIManager.Inst().GetUI(UIEnum.Squad);
            SelectUI selectUI = (SelectUI)UIManager.Inst().GetUI(UIEnum.Select);
            SelectPortraitUI selectPortraitUI = selectUI.GetSelectedSelectPortraitUI();
            int heroIndex = -1;

            //표시 여부 설정
            if (selectPortraitUI == null)
            {
                if (infoPanel)
                    infoPanel.gameObject.SetActive(false);

                if (back)
                    back.gameObject.SetActive(true);

                return;
            }
            else
            {
                if (infoPanel)
                    infoPanel.gameObject.SetActive(true);

                if (back)
                    back.gameObject.SetActive(false);

                heroIndex = selectUI.GetSelectedSelectPortraitIndex();
            }

            PlayerManager playerManger = PlayerManager.Inst();
            List<UnitRoot> unitRoots = playerManger.GetUnitRoots();

            Unit unit = unitRoots[heroIndex].GetUnit();
            UnitMeta unitMeta = unit.GetUnitMeta();
            UnitData unitData = unit.GetData();
            List<Weapon> weapons = unit.GetWeaponList();
            Hero hero = unit?.GetStatus<Hero>();
            HeroMeta heroMeta = hero?.GetHeroMeta();
            HeroData heroData = hero?.GetData();
            Damage damage = unit.GetDamage();
            int attributeNum = (int)unitMeta.GetAttribute();

            if (squadUI)
            {

                if (charName)
                {
                    charName.text = heroMeta.GetCharName();
                }

                if (levelMax)
                {
                    levelMax.text = (heroMeta.GetHeroExtraStatDatas().Count - 1).ToString();
                }

                if (level)
                {
                    level.text = heroData.GetLevel().ToString();
                }

                if (attribute)
                {
                    attribute.sprite = charUIMeta.GetAttributeLargeImage(attributeNum);
                }

                if (health)
                {
                    health.text = ((int)unitData.GetHpMax()).ToString();
                }

                if (attack && damage)
                {
                    attack.text = ((int)damage.ReturnUpdateDamageValue()).ToString();
                }

                if (defense && magicResistance && unit)
                {
                    defense.text = ((int)unit.GetData().GetDefense(DamageType.Physics)).ToString();
                    magicResistance.text = ((int)unit.GetData().GetDefensePer(DamageType.Magic)).ToString();
                }

                if (respwan)
                {
                    if (heroData.GetRespawnTime() > 48.0f)
                    {
                        respwan.text = "매우느림";
                    }
                    else if (heroData.GetRespawnTime() > 36.0f)
                    {
                        respwan.text = "느림";
                    }
                    else if (heroData.GetRespawnTime() > 24.0f)
                    {
                        respwan.text = "보통";
                    }
                    else if (heroData.GetRespawnTime() > 12.0f)
                    {
                        respwan.text = "빠름";
                    }
                    else
                    {
                        respwan.text = "매우빠름";
                    }
                }

                if (cost)
                {
                    cost.text = unitData.GetCost().ToString();
                }

                if (block)
                {
                    block.text = unitData.GetBlock().ToString();
                }

                if (attackSpeed)
                {
                    attackSpeed.text = "빠름"; //재수정 필요함
                }

            }
        }
    }
}
