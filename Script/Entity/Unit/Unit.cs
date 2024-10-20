using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Profiling.Memory.Experimental;
using static Spine.Unity.Examples.SpineboyFootplanter;
using static UnityEngine.CompositeCollider2D;
using static UnityEngine.UI.CanvasScaler;

namespace Data
{
    [Serializable]
    public class Unit : Base
    {
        [Header("- 자식 데이터")]
        [SerializeField, ReadOnly] private List<Weapon> weapons = new List<Weapon>();
        [SerializeField] private List<Weapon> weaponPrefabs = new List<Weapon>();

        [SerializeField, ReadOnly] private List<Ability> abilities = new List<Ability>();
        [SerializeField] private List<Ability> abilityPrefabs = new List<Ability>();

        [SerializeField, ReadOnly] private List<Status> statuses = new List<Status>();
        [SerializeField] private List<Status> statusPrefabs = new List<Status>();

        [SerializeField, ReadOnly] private Damage damage;
        [SerializeField] private Damage damagePrefab;

        [Header("- 메타 데이터")]
        [SerializeField] private UnitMeta unitMeta;

        [Header("- 인게임 데이터")]
        [SerializeField, ReadOnly] private UnitData unitData;

        public List<Weapon> GetWeaponList() => weapons;
        public List<Ability> GetAbilityList() => abilities;
        public List<Status> GetStatusList() => statuses;
        public Damage GetDamage() => damage;

        public override BaseMeta GetMeta() => unitMeta;
        public UnitMeta GetUnitMeta() => unitMeta;
        public UnitData GetData() => unitData;

        public override void CreateChild(Team team)
        {
            //팀 설정
            gameObject.layer = (int)team + 6;

            //자식 오브젝트 생성
            PoolManager poolManager = PoolManager.Inst();

            for (int i = 0; i < weaponPrefabs.Count; i++)
            {
                weapons.Add((Weapon)poolManager.GetPoolObj(weaponPrefabs[i].gameObject, this, team));
            }
            for (int i = 0; i < abilityPrefabs.Count; i++)
            {
                abilities.Add((Ability)poolManager.GetPoolObj(abilityPrefabs[i].gameObject, this, team));
            }
            for (int i = 0; i < statusPrefabs.Count; i++)
            {
                statuses.Add((Status)poolManager.GetPoolObj(statusPrefabs[i].gameObject, this, team));
            }
            if (damagePrefab)
            {
                damage = (Damage)poolManager.GetPoolObj(damagePrefab.gameObject, this, team);
            }
        }
        public override void StartEvent()
        {
            //이벤트 실행
            UnitBirth();

            //하위
            for (int i = 0; i < weapons.Count; i++)
            {
                weapons[i].StartEvent();
            }
            for (int i = 0; i < abilities.Count; i++)
            {
                abilities[i].StartEvent();
            }
            for (int i = 0; i < statuses.Count; i++)
            {
                statuses[i].StartEvent();
            }
            if (damage)
            {
                damage.StartEvent();
            }
        }
        public override void UpdateDataAll()
        {
            //업데이트 실행
            UpdateData();

            //하위
            for(int i = 0; i < weaponPrefabs.Count; i++)
            {
                weapons[i].UpdateDataAll();
            }
            for (int i = 0; i < abilityPrefabs.Count; i++)
            {
                abilities[i].UpdateDataAll();
            }
            for (int i = 0; i < statusPrefabs.Count; i++)
            {
                statuses[i].UpdateDataAll();
            }
            if(damagePrefab)
            {
                damage.UpdateDataAll();
            }

        }
        public override void UpdateData()
        {
            //데이터 주기적 초기화
            UnitData data = unitMeta.GetUnitData();
            unitData.SetHpMax((data.GetHpMax() * unitData.GetHpExtraRatioUnscaled() + unitData.GetHpExtraBonus()) * unitData.GetHpExtraRatioScaled());
            unitData.SetHp(data.GetHp() > data.GetHpMax() ? data.GetHpMax() : (data.GetHp() * unitData.GetHpExtraRatioUnscaled() + unitData.GetHpExtraBonus()) * unitData.GetHpExtraRatioScaled());
            unitData.SetMpMax((data.GetMp() * unitData.GetMpExtraRatioUnscaled() + unitData.GetMpExtraBonus()) * unitData.GetMpExtraRatioScaled());
            unitData.SetMp(data.GetMp() > data.GetMpMax() ? data.GetMpMax() : (data.GetMp() * unitData.GetMpExtraRatioUnscaled() + unitData.GetMpExtraBonus()) * unitData.GetMpExtraRatioScaled());
            unitData.SetMoveSpeed(data.GetMoveSpeed());
            unitData.SetCost(data.GetCost());
            unitData.SetBlock(data.GetBlock());
            unitData.SetAtkPeriod(data.GetAtkPeriod());

            for (DamageType i = DamageType.Physics; i < DamageType.End; i++)
            {
                unitData.SetDefense((data.GetDefense(i) *(1.0f + unitData.GetDefenseExtraRatioUnscaled(i)) + unitData.GetDefenseExtraBonus(i)) * (1.0f + unitData.GetDefenseExtraRatioScaled(i)), (DamageType)i);
                unitData.SetDefensePer((data.GetDefensePer(i) *(1.0f + unitData.GetDefensePerExtraRatioUnscaled(i)) + unitData.GetDefensePerExtraBonus(i)) * (1.0f + unitData.GetDefensePerExtraRatioScaled(i)), (DamageType)i);
            }
        }
        public void UpdateStatus()
        {
            UnitData data = unitMeta.GetUnitData();

            float hpMaxPrev = unitData.GetHpMax();
            float hpMaxNext = (data.GetHpMax() * unitData.GetHpExtraRatioUnscaled() + unitData.GetHpExtraBonus()) * unitData.GetHpExtraRatioScaled();
            float hpMaxInterval = hpMaxNext - hpMaxPrev;
            unitData.SetHpMax(hpMaxNext);
            if (hpMaxInterval >= 0)
                unitData.SetHp(unitData.GetHp() + hpMaxInterval);
            if (unitData.GetHpMax() < 1.0f)
                unitData.SetHpMax(1.0f);
            if (unitData.GetHp() > unitData.GetHpMax())
                unitData.SetHp(unitData.GetHpMax());

            float mpMaxPrev = unitData.GetMpMax();
            float mpMaxNext = (data.GetMp() * unitData.GetMpExtraRatioUnscaled() + unitData.GetMpExtraBonus()) * unitData.GetMpExtraRatioScaled();
            float mpMaxInterval = mpMaxNext - mpMaxPrev;
            unitData.SetMpMax(mpMaxNext);
            if (mpMaxInterval >= 0)
                unitData.SetMp(unitData.GetMp() + mpMaxInterval);
            if (unitData.GetMpMax() < 1.0f)
                unitData.SetMpMax(1.0f);
            if (unitData.GetMp() > unitData.GetMpMax())
                unitData.SetMp(unitData.GetMpMax());

            for (DamageType i = DamageType.Physics; i < DamageType.End; i++)
            {
                unitData.SetDefense((data.GetDefense(i) * (1.0f + unitData.GetDefenseExtraRatioUnscaled(i)) + unitData.GetDefenseExtraBonus(i)) * (1.0f + unitData.GetDefenseExtraRatioScaled(i)), (DamageType)i);
                unitData.SetDefensePer((data.GetDefensePer(i) * (1.0f + unitData.GetDefensePerExtraRatioUnscaled(i)) + unitData.GetDefensePerExtraBonus(i)) * (1.0f + unitData.GetDefensePerExtraRatioScaled(i)), (DamageType)i);
            }
        }

        public override void PreDestroy()
        {
            //하위 오브젝트 해제
            for (int i = 0; i < weapons.Count; i++)
            {
                weapons[i].PreDestroy();
            }
            for (int i = 0; i < abilities.Count; i++)
            {
                abilities[i].PreDestroy();
            }
            for (int i = 0; i < statuses.Count; i++)
            {
                statuses[i].PreDestroy();
            }
            DestroyManager.Inst().Destroy(this);
        }

        public override void LastDestroy()
        {
            if (gameObject.activeSelf == false)
                return;

            weapons.Clear();
            abilities.Clear();
            statuses.Clear();
            PoolManager.Inst().ReleaseObj(gameObject, unitMeta);
        }

        public void UnitBirth()
        {
            CreatorManager.Inst().EventStart(MessageType.UnitBirth, unitMeta, "", rootBase, rootBase, rootBase);
            rootBase.EventStart(MessageType.UnitBirth, unitMeta, "", rootBase, rootBase, rootBase);
        }

        public void Damage(float amount, DamageType damageType)
        {
            if (unitMeta == null)
                return;

            float damage = amount;

            damage -= damage * unitData.GetDefensePer(damageType);
            damage -= unitData.GetDefense(damageType);

            if (damage < 0.0f)
                damage = 0.0f;

            float hp = unitData.GetHp() - damage;

            if(hp > 1.0f)
            {
                unitData.SetHp(hp);
            }
            else
            {
                rootBase.PreDestroy();
                unitData.SetHp(0.0f);
            }

            //유닛 피해 받음 이벤트
            rootBase.EventStart(MessageType.UnitDamaged, unitMeta, "", rootBase, rootBase, rootBase);
            rootBase.EventStart(MessageType.UnitDamaged, null, "", rootBase, rootBase, rootBase);
        }
        public Ability SearchSkillAbility(int index)
        {
            int num = 0;

            for(int i = 0; i < abilities.Count; i++)
            {
                if (abilities[i].GetComponent<TargetAbil>() || abilities[i].GetComponent<InstantAbil>())
                {
                    if(num == index)
                    {
                        return abilities[i];
                    }
                    else
                    {
                        num++;
                    }
                }
            }

            return null;
        }

        public T GetStatus<T>() where T : Status
        {
            for (int i = 0; i < statuses.Count; i++)
            {
                T component = statuses[i].GetComponent<T>();

                if (component)
                {
                    return component;
                }
            }

            return null;
        }

        public void GetStatuses<T>(List<T> inout) where T : Status
        {
            for (int i = 0; i < statuses.Count; i++)
            {
                T component = statuses[i].GetComponent<T>();

                if (component)
                {
                    inout.Add(component);
                }
            }
        }
    }
}

