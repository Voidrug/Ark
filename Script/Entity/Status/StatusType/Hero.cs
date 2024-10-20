using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class Hero : Status
    {
        [Header("- 메타 데이터")]
        [SerializeField] private HeroMeta heroMeta;

        [Header("- 인게임 데이터")]
        [SerializeField, ReadOnly] private HeroData heroData;

        public override BaseMeta GetMeta() => heroMeta;
        public HeroMeta GetHeroMeta() => heroMeta;
        public HeroData GetData() => heroData;

        public override void CreateChild(Team team)
        {
        }
        public override void UpdateDataAll()
        {
            //업데이트 실행
            UpdateData();
        }
        public override void UpdateData()
        {
            //기본 데이터 적용
            HeroData data = heroMeta.GetHeroData();

            heroData.SetExp(data.GetExp());
            heroData.SetLevel(data.GetLevel());
            heroData.SetRespawnTime(data.GetRespawnTime());

            ApplyData();
        }
        public override void StartEvent()
        {
            
        }

        public override void PreDestroy()
        {
            RemoveData();

            DestroyManager.Inst().Destroy(this);
        }

        public override void LastDestroy()
        {
            if (gameObject.activeSelf == false)
                return;

            PoolManager.Inst().ReleaseObj(gameObject, heroMeta);
        }

        public void ApplyExp(int exp)
        {
            HeroExtraStatData heroExtraStatData = heroMeta.GetHeroExtraStatDatas()[heroData.GetLevel()];
            ExtraStatData extraStat = heroExtraStatData.GetExtraStatData();

            RemoveData();

            heroData.SetExp(heroData.GetExp() + exp);

            if (heroData.GetExp() >= 0)
            {
                //경험치 설정
                while (heroData.GetExp() >= heroExtraStatData.GetNeedExp() && heroData.GetLevel() < heroMeta.GetHeroExtraStatDatas().Count)
                {
                    heroData.SetExp(heroData.GetExp() - heroExtraStatData.GetNeedExp());
                    heroData.SetLevel(heroData.GetLevel() + 1);
                    heroExtraStatData = heroMeta.GetHeroExtraStatDatas()[heroData.GetLevel()];
                }

                //최대 레벨일때 경험치 획득 무시
                if (heroData.GetLevel() == heroMeta.GetHeroExtraStatDatas().Count && heroData.GetExp() > 0)
                {
                    heroData.SetExp(0);
                }
            }
            else
            {
                //경험치 설정
                while (heroData.GetExp() < 0 && heroData.GetLevel() > 0)
                {
                    heroExtraStatData = heroMeta.GetHeroExtraStatDatas()[heroData.GetLevel() - 1];
                    heroData.SetExp(heroData.GetExp() + heroExtraStatData.GetNeedExp());
                    heroData.SetLevel(heroData.GetLevel() - 1);
                }

                //최저 레벨일때 경험치 삭감 무시
                if (heroData.GetLevel() == 0 && heroData.GetExp() < 0)
                {
                    heroData.SetExp(0);
                }
            }
        }
        public void ApplyData()
        {
            Unit unit = transform.parent.GetComponent<Unit>();
            UnitData unitData = unit?.GetData();
            ExtraStatData extraStatData = heroMeta.GetHeroExtraStatDatas()[heroData.GetLevel()].GetExtraStatData();

            if(unitData != null)
            {
                unitData.AddHpMaxExtraBonus(extraStatData.GetHpExtraBonus());
                unitData.AddHpMaxExtraRatioUnscaled(extraStatData.GetHpExtraRatioUnscaled());
                unitData.AddHpMaxExtraRatioScaled(extraStatData.GetHpExtraRatioScaled());
                unitData.AddMpMaxExtraBonus(extraStatData.GetMpExtraBonus());
                unitData.AddMpMaxExtraRatioUnscaled(extraStatData.GetMpExtraRatioUnscaled());
                unitData.AddMpMaxExtraRatioScaled(extraStatData.GetMpExtraRatioScaled());

                for (DamageType i = 0; i < DamageType.End; i++)
                {
                    unitData.AddDefenseExtraBonus(extraStatData.GetDefenseExtraBonus(i), i);
                    unitData.AddDefenseExtraRatioUnscaled(extraStatData.GetDefenseExtraRatioUnscaled(i), i);
                    unitData.AddDefenseExtraRatioScaled(extraStatData.GetDefenseExtraRatioScaled(i), i);
                    unitData.AddDefensePerExtraBonus(extraStatData.GetDefensePerExtraBonus(i), i);
                    unitData.AddDefensePerExtraRatioUnscaled(extraStatData.GetDefensePerExtraRatioUnscaled(i), i);
                    unitData.AddDefensePerExtraRatioScaled(extraStatData.GetDefensePerExtraRatioScaled(i), i);

                    unitData.AddDamageExtraRatio(extraStatData.GetDamageExtraRatio(i), i);
                }

                for (Attribute i = 0; i < Attribute.End; i++)
                {
                    unitData.AddAmountExtraBonus(extraStatData.GetAmountExtraBonus(i), i);
                    unitData.AddAmountExtraRaioScaled(extraStatData.GetAmountExtraRatioScaled(i), i);
                    unitData.AddAmountExtraRatioUnscaled(extraStatData.GetAmountExtraRatioUnscaled(i), i);
                }



                unit.UpdateData();
            }
        }
        public void RemoveData()
        {
            Unit unit = transform.parent.GetComponent<Unit>();
            UnitData unitData = unit?.GetData();
            ExtraStatData extraStatData = heroMeta.GetHeroExtraStatDatas()[heroData.GetLevel()].GetExtraStatData();

            if (unitData != null)
            {
                unitData.AddHpMaxExtraBonus(-extraStatData.GetHpExtraBonus());
                unitData.AddHpMaxExtraRatioUnscaled(-extraStatData.GetHpExtraRatioUnscaled());
                unitData.AddHpMaxExtraRatioScaled(-extraStatData.GetHpExtraRatioScaled());
                unitData.AddMpMaxExtraBonus(-extraStatData.GetMpExtraBonus());
                unitData.AddMpMaxExtraRatioUnscaled(-extraStatData.GetMpExtraRatioUnscaled());
                unitData.AddMpMaxExtraRatioScaled(-extraStatData.GetMpExtraRatioScaled());

                for (DamageType i = 0; i < DamageType.End; i++)
                {
                    unitData.AddDefenseExtraBonus(-extraStatData.GetDefenseExtraBonus(i), i);
                    unitData.AddDefenseExtraRatioUnscaled(-extraStatData.GetDefenseExtraRatioUnscaled(i), i);
                    unitData.AddDefenseExtraRatioScaled(-extraStatData.GetDefenseExtraRatioScaled(i), i);
                    unitData.AddDefensePerExtraBonus(-extraStatData.GetDefensePerExtraBonus(i), i);
                    unitData.AddDefensePerExtraRatioUnscaled(-extraStatData.GetDefensePerExtraRatioUnscaled(i), i);
                    unitData.AddDefensePerExtraRatioScaled(-extraStatData.GetDefensePerExtraRatioScaled(i), i);
                }

                unit.UpdateData();
            }


        }
    }
}
