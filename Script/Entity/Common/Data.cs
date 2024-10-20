using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

namespace Data
{
    [Serializable]
    public sealed class RootEvent : UltEvents.UltEvent<Root,Actor> { }

    [Serializable]
    public struct EventKey
    {
        [Header("- 이벤트")]
        [SerializeField] private MessageType messageType;
        [SerializeField] private ScriptableObject metaData;
        [SerializeField] private string message;

        public EventKey(MessageType type, ScriptableObject data, string str)
        {
            messageType = type;
            metaData = data;
            message = str;
        }

        public MessageType GetMessageType() => messageType;
        public ScriptableObject GetMetaData() => metaData;
        public string GetMessage() => message;
        public void SetMessageType(MessageType value) => messageType = value;
        public void SetMetaData(ScriptableObject value) => metaData = value;
        public void SetMessage(string value) => message = value;
    }

    [Serializable]
    public class EventValue
    {
        [Header("- 조건")]
        [SerializeField] private List<Condition> conditions;

        [Header("- 행동")]
        [SerializeField] private UltEvent<Root, Actor> actions;

        public List<Condition> GetConditions() => conditions;
        public UltEvent<Root, Actor> GetActions() => actions;
    }

    [Serializable]
    public class Condition
    {
        [SerializeField] private ConditionType conditionType;

        [Header("- Location")]
        [SerializeField] private Location location;

        [Header("- String")]
        [SerializeField] private string str;

        public ConditionType GetConditionType() => conditionType;
        public Location GetLocation() => location;

        public string GetStr() => str;
    }

    [System.Serializable]
    public class EventDict : SerializableDictionaryBase<EventKey, EventValue> { }

    [Serializable]
    public class Accumulator
    {
        [SerializeField] private Operator oper;
        [SerializeField] private AccumulatorMeta accumulatorMeta;

        public AccumulatorMeta GetAccumulatorMeta() => accumulatorMeta;
        public Operator GetOperator() => oper;
    }
    public class RootData
    {
        [Header("- Team")]
        [SerializeField] private Team team;

        public Team GetTeam() => team;
        public void SetTeam(Team index) => team = index; 
    }

    [Serializable]
    public class UnitRootData : RootData
    {

    }

    [Serializable]
    public class EffectRootData : RootData
    {

    }

    public class CreatorData
    {

    }

    [Serializable]
    public class UnitCreatorData : CreatorData
    {

    }

    [Serializable]
    public class EffectCreatorData : CreatorData
    {

    }

    public class ActorData
    {

    }

    [Serializable]
    public class ModelData : ActorData
    {

    }
    [Serializable]
    public class StatusBarData : ActorData
    {

    }
    [Serializable]
    public class AoeData : ActorData
    {

    }


    [Serializable]
    public class UnitData
    {
        [Header("- 체력")]
        [SerializeField] private float hp = 1.0f;

        [Header("- 체력 최대값")]
        [SerializeField] private float hpMax = 1.0f;

        [Header("- 체력 추가 보너스")]
        private float hpExtraBonus = 0.0f;

        [Header("- 체력 추가 비율 조정안됨")]
        private float hpExtraRatioUnscaled = 1.0f;

        [Header("- 체력 추가 비율 조정됨")]
        private float hpExtraRatioScaled = 1.0f;

        [Header("- 마나")]
        [SerializeField] private float mp = 0.0f;

        [Header("- 마나최대값")]
        [SerializeField] private float mpMax = 0.0f;

        [Header("- 마나 추가 보너스")]
        private float mpExtraBonus = 0.0f;

        [Header("- 마나 추가 비율 조정안됨")]
        private float mpExtraRatioUnscaled = 1.0f;

        [Header("- 마나 추가 비율 조정됨")]
        private float mpExtraRatioScaled = 1.0f;

        [Header("- 방어력")]
        [Header("  0:Phy, 1:Magic, 2:True, 3:Element")]
        [SerializeField] private float[] defense = new float[(int)DamageType.End] {0.0f, 0.0f, 0.0f, 0.0f};

        [Header("- 방어력 추가 보너스")]
        [Header("  0:Phy, 1:Magic, 2:True, 3:Element")]
        private float[] defenseExtraBonus = new float[(int)DamageType.End] { 0.0f, 0.0f, 0.0f, 0.0f };

        [Header("- 방어력 추가 비율 조정안됨")]
        [Header("  0:Phy, 1:Magic, 2:True, 3:Element")]
        private float[] defenseExtraRatioUnscaled = new float[(int)DamageType.End] { 0.0f, 0.0f, 0.0f, 0.0f };

        [Header("- 방어력 추가 비율 조정됨")]
        [Header("  0:Phy, 1:Magic, 2:True, 3:Element")]
        private float[] defenseExtraRatioScaled = new float[(int)DamageType.End] { 0.0f, 0.0f, 0.0f, 0.0f };

        [Header("- 방어력%")]
        [Header("  0:Phy, 1:Magic, 2:True, 3:Element")]
        [SerializeField] private float[] defensePer = new float[(int)DamageType.End] { 0.0f, 0.0f, 0.0f, 0.0f };

        [Header("- 방어력% 추가 보너스")]
        [Header("  0:Phy, 1:Magic, 2:True, 3:Element")]
        private float[] defensePerExtraBonus = new float[(int)DamageType.End] { 0.0f, 0.0f, 0.0f, 0.0f };

        [Header("- 방어력% 추가 비율 조정안됨")]
        [Header("  0:Phy, 1:Magic, 2:True, 3:Element")]
        private float[] defensePerExtraRatioUnscaled = new float[(int)DamageType.End] { 0.0f, 0.0f, 0.0f, 0.0f };

        [Header("- 방어력% 추가 비율 조정됨")]
        [Header("  0:Phy, 1:Magic, 2:True, 3:Element")]
        private float[] defensePerExtraRatioScaled = new float[(int)DamageType.End] { 0.0f, 0.0f, 0.0f, 0.0f };

        [Header("- 특성피해 추가 보너스")]
        [Header(" 0:No, 1:Gu, 2:Ca, 3:Va, 4:Su, 5:Me, 6:De, 7:Sn, 8:Sp")]
        private float[] amountExtraBonus = new float[(int)Attribute.End] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

        [Header("- 특성피해 추가 비율 조정안됨")]
        [Header(" 0:No, 1:Gu, 2:Ca, 3:Va, 4:Su, 5:Me, 6:De, 7:Sn, 8:Sp")]
        private float[] amountExtraRatioUnscaled = new float[(int)Attribute.End] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

        [Header("- 특성피해 추가 비율 조정됨")]
        [Header(" 0:No, 1:Gu, 2:Ca, 3:Va, 4:Su, 5:Me, 6:De, 7:Sn, 8:Sp")]
        private float[] amountExtraRatioScaled = new float[(int)Attribute.End] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

        [Header("- 속성피해 추가 비율")]
        [Header("  0:Phy, 1:Magic, 2:True, 3:Element")]
        private float[] damageExtraRatio = new float[(int)DamageType.End] { 0.0f, 0.0f, 0.0f, 0.0f };

        [Header("- 이동속도")]
        [SerializeField] private float moveSpeed = 0.0f;

        [Header("- 저지수")]
        [SerializeField] private uint block = 1;

        [Header("- 비용")]
        [SerializeField] private uint cost = 1;

        [Header("- 공격우선순위")] // 값이 높을 수록 우선적으로 공격받습니다
        [SerializeField] private uint atkPeriod = 0;

        public UnitData()
        {

        }

        public void SetHp(float value) => hp = value;
        public void SetHpMax(float value) => hpMax = value;
        public void SetHpExtraBonus(float value) => hpExtraBonus = value;
        public void SethpExtraRatioUnscaled(float value) => hpExtraRatioUnscaled = value;
        public void SethpExtraRatioScaled(float value) => hpExtraRatioScaled = value;
        public void SetMp(float value) => mp = value;
        public void SetMpMax(float value) => mpMax = value;
        public void SetMpExtraBonus(float value) => mpExtraBonus = value;
        public void SexMpExtraRatioUnscaled(float value) => mpExtraRatioUnscaled = value;
        public void SexMpExtraRatioScaled(float value) => mpExtraRatioScaled = value;
        public void SetDefense(float value, DamageType damageType) => defense[(int)damageType] = value;
        public void SetDefenseExtraBonus(float value, DamageType damageType) => defenseExtraBonus[(int)damageType] = value;
        public void SetDefenseExtraRatioUnscaled(float value, DamageType damageType) => defenseExtraRatioUnscaled[(int)damageType] = value;
        public void SetDefenseExtraRatioScaled(float value, DamageType damageType) => defenseExtraRatioScaled[(int)damageType] = value;
        public void SetDefensePer(float value, DamageType damageType) => defensePer[(int)damageType] = value;
        public void SetDefensePerExtraBonus(float value, DamageType damageType) => defensePerExtraBonus[(int)damageType] = value;
        public void SetDefensePerExtraRatioUnscaled(float value, DamageType damageType) => defensePerExtraRatioUnscaled[(int)damageType] = value;
        public void SetDefensePerExtraRatioScaled(float value, DamageType damageType) => defensePerExtraRatioScaled[(int)damageType] = value;
        public void SetAmountExtraBonus(float value, Attribute attribute) => amountExtraBonus[(int)attribute] = value;
        public void SetAmountExtraRatioUnscaled(float value, Attribute attribute) => amountExtraRatioUnscaled[(int)attribute] = value;
        public void SetAmountExtraRaioScaled(float value, Attribute attribute) => amountExtraRatioScaled[(int)attribute] = value;
        public void SetDamageExtraRatio(float value, DamageType damageType) => damageExtraRatio[(int)damageType] = value;
        public void SetMoveSpeed(float value) => moveSpeed = value;
        public void SetBlock(uint value) => block = value;
        public void SetCost(uint value) => cost = value;
        public void SetAtkPeriod(uint value) => atkPeriod = value;

        public void AddHpMaxExtraBonus(float value) => hpExtraBonus += value;
        public void AddHpMaxExtraRatioUnscaled(float value) => hpExtraRatioUnscaled += value;
        public void AddHpMaxExtraRatioScaled(float value) => hpExtraRatioScaled += value;
        public void AddMpMaxExtraBonus(float value) => mpExtraBonus += value;
        public void AddMpMaxExtraRatioUnscaled(float value) => mpExtraRatioUnscaled += value;
        public void AddMpMaxExtraRatioScaled(float value) => mpExtraRatioScaled += value;
        public void AddDefenseExtraBonus(float value, DamageType damageType) => defenseExtraBonus[(int)damageType] += value;
        public void AddDefenseExtraRatioUnscaled(float value, DamageType damageType) => defenseExtraRatioUnscaled[(int)damageType] += value;
        public void AddDefenseExtraRatioScaled(float value, DamageType damageType) => defenseExtraRatioScaled[(int)damageType] += value;
        public void AddDefensePerExtraBonus(float value, DamageType damageType) => defensePerExtraBonus[(int)damageType] += value;
        public void AddDefensePerExtraRatioUnscaled(float value, DamageType damageType) => defensePerExtraRatioUnscaled[(int)damageType] += value;
        public void AddDefensePerExtraRatioScaled(float value, DamageType damageType) => defensePerExtraRatioScaled[(int)damageType] += value;
        public void AddAmountExtraBonus(float value, Attribute attribute) => amountExtraBonus[(int)attribute] += value;
        public void AddAmountExtraRatioUnscaled(float value, Attribute attribute) => amountExtraRatioUnscaled[(int)attribute] += value;
        public void AddAmountExtraRaioScaled(float value, Attribute attribute) => amountExtraRatioScaled[(int)attribute] += value;
        public void AddDamageExtraRatio(float value, DamageType damageType) => damageExtraRatio[(int)damageType] += value;

        public float GetHp() => hp;
        public float GetHpMax() => hpMax;
        public float GetHpExtraBonus() => hpExtraBonus;
        public float GetHpExtraRatioUnscaled() => hpExtraRatioUnscaled;
        public float GetHpExtraRatioScaled() => hpExtraRatioScaled;
        public float GetMp() => mp;
        public float GetMpMax() => mpMax;
        public float GetMpExtraBonus() => mpExtraBonus;
        public float GetMpExtraRatioUnscaled() => mpExtraRatioUnscaled;
        public float GetMpExtraRatioScaled() => mpExtraRatioScaled;
        public float GetDefense(DamageType damageType) => defense[(int)damageType];
        public float GetDefenseExtraBonus(DamageType damageType) => defenseExtraBonus[(int)damageType];
        public float GetDefenseExtraRatioUnscaled(DamageType damageType) => defenseExtraRatioUnscaled[(int)damageType];
        public float GetDefenseExtraRatioScaled(DamageType damageType) => defenseExtraRatioScaled[(int)damageType];
        public float GetDefensePer(DamageType damageType) => defensePer[(int)damageType];
        public float GetDefensePerExtraBonus(DamageType damageType) => defensePerExtraBonus[(int)damageType];
        public float GetDefensePerExtraRatioUnscaled(DamageType damageType) => defensePerExtraRatioUnscaled[(int)damageType];
        public float GetDefensePerExtraRatioScaled(DamageType damageType) => defensePerExtraRatioScaled[(int)damageType];
        public float GetAmountExtraBonus(Attribute attribute) => amountExtraBonus[(int)attribute];
        public float GetAmountExtraRatioUnscaled(Attribute attribute) => amountExtraRatioUnscaled[(int)attribute];
        public float GetAmountExtraRatioScaled(Attribute attribute) => amountExtraRatioScaled[(int)attribute];
        public float GetDamageExtraRatio(DamageType damageType) => damageExtraRatio[(int)damageType];
        public float GetMoveSpeed() => moveSpeed;
        public uint GetBlock() => block;
        public uint GetCost() => cost;
        public uint GetAtkPeriod() => atkPeriod;
    }


    [Serializable]
    public class WeaponData
    {
        [Header("- 공격속도"), Range(0.1f,50.0f)]
        [SerializeField] private float atkSpeed = 1.0f;

        [Header("- 무기우선순위")] // 값이 높을 수록 우선적으로 사용하는 무기입니다
        [SerializeField] private uint period;

        [Header("- 무기사용안함")]
        [SerializeField] private bool disable;



        public float GetAtkSpeed() => atkSpeed;
        public uint GetPeriod() => period;
        public bool GetDisable() => disable;
        public void SetAtkSpeed(float value) => atkSpeed = value;
        public void SetPeriod(uint value) => period = value;
        public void SetDisable(bool value) => disable = value;
    }

    public class AbilityData
    {
        [Header("- 비용")]
        [SerializeField] private float hpCost;
        [SerializeField] private float mpCost;


    }
    [Serializable]
    public class TargetAbilData : AbilityData
    {

    }
    [Serializable]
    public class MoveData : AbilityData
    {

    }
    [Serializable]
    public class InstantAbilData : AbilityData
    {

    }

    public class StatusData
    {

    }

    [Serializable]
    public class HeroData : StatusData
    {
        [Header("- 레벨")]
        [SerializeField] private int level = 0;

        [Header("- 경험치")]
        [SerializeField] private int exp = 0;

        [Header("- 부활대기시간")]
        [SerializeField] private float respawnTime = 10.0f;

        public int GetLevel() => level;
        public int GetExp() => exp;
        public float GetRespawnTime() => respawnTime;
        public void SetLevel(int value) => level = value;
        public void SetExp(int value) => exp = value;
        public void SetRespawnTime(float value) => respawnTime = value;
    }
    [Serializable]
    public class BuffData : StatusData
    {

    }

    [Serializable]
    public class HeroExtraStatData : StatusData
    {
        [Header("- 필요 경험치")]
        [SerializeField] private int needExp = 0;

        [Header("- 추가 스탯")]
        [SerializeField] private ExtraStatData extraStat = new ExtraStatData();

        public int GetNeedExp() => needExp;
        public ExtraStatData GetExtraStatData() => extraStat;
    }


    [Serializable]
    public class ExtraStatData
    {
        [Header("- 체력 추가 보너스")]
        [SerializeField] private float hpExtraBonus = 0;

        [Header("- 체력 추가 비율 조정안됨")]
        [SerializeField] private float hpExtraRatioUnscaled = 1.0f;

        [Header("- 체력 추가 비율 조정됨")]
        [SerializeField] private float hpExtraRatioScaled = 1.0f;

        [Header("- 마나 추가 보너스")]
        [SerializeField] private float mpExtraBonus = 0;

        [Header("- 마나 추가 비율 조정안됨")]
        [SerializeField] private float mpExtraRatioUnscaled = 1.0f;

        [Header("- 마나 추가 비율 조정됨")]
        [SerializeField] private float mpExtraRatioScaled = 1.0f;

        [Header("- 방어력 추가 보너스")]
        [SerializeField] private float[] defenseExtraBonus = new float[(int)DamageType.End] {0.0f,0.0f,0.0f,0.0f };

        [Header("- 방어력 추가 비율 조정안됨")]
        [SerializeField] private float[] defenseExtraRatioUnscaled = new float[(int)DamageType.End] {0.0f,0.0f,0.0f,0.0f };

        [Header("- 방어력 추가 비율 조정됨")]
        [SerializeField] private float[] defenseExtraRatioScaled = new float[(int)DamageType.End] { 0.0f, 0.0f, 0.0f, 0.0f };

        [Header("- 방어력% 추가 보너스")]
        [SerializeField] private float[] defensePerExtraBonus = new float[(int)DamageType.End] { 0.0f, 0.0f, 0.0f, 0.0f };

        [Header("- 방어력% 추가 비율 조정안됨")]
        [SerializeField] private float[] defensePerExtraRatioUnscaled = new float[(int)DamageType.End] { 0.0f, 0.0f, 0.0f, 0.0f };

        [Header("- 방어력% 추가 비율 조정됨")]
        [SerializeField] private float[] defensePerExtraRatioScaled = new float[(int)DamageType.End] { 0.0f, 0.0f, 0.0f, 0.0f };

        [Header("- 특성피해 추가 보너스")]
        [Header(" 0:No, 1:Gu, 2:Ca, 3:Va, 4:Su, 5:Me, 6:De, 7:Sn, 8:Sp")]
        [SerializeField] private float[] amountExtraBonus = new float[(int)Attribute.End] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

        [Header("- 특성피해 추가 비율 조정안됨")]
        [Header(" 0:No, 1:Gu, 2:Ca, 3:Va, 4:Su, 5:Me, 6:De, 7:Sn, 8:Sp")]
        [SerializeField] private float[] amountExtraRatioUnscaled = new float[(int)Attribute.End] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

        [Header("- 특성피해 추가 비율 조정됨")]
        [Header(" 0:No, 1:Gu, 2:Ca, 3:Va, 4:Su, 5:Me, 6:De, 7:Sn, 8:Sp")]
        [SerializeField] private float[] amountExtraRatioScaled = new float[(int)Attribute.End] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
        
        [Header("- 속성피해 추가 비율")]
        [Header("  0:Phy, 1:Magic, 2:True, 3:Element")]
        [SerializeField] private float[] damageExtraRatio = new float[(int)DamageType.End] { 0.0f, 0.0f, 0.0f, 0.0f };

        public float GetHpExtraBonus() => hpExtraBonus;
        public float GetHpExtraRatioUnscaled() => hpExtraRatioUnscaled;
        public float GetHpExtraRatioScaled() => hpExtraRatioScaled;
        public float GetMpExtraBonus() => mpExtraBonus;
        public float GetMpExtraRatioUnscaled() => mpExtraRatioUnscaled;
        public float GetMpExtraRatioScaled() => mpExtraRatioScaled;
        public float GetDefenseExtraBonus(DamageType damageType) => defenseExtraBonus[(int)damageType];
        public float GetDefenseExtraRatioUnscaled(DamageType damageType) => defenseExtraRatioUnscaled[(int)damageType];
        public float GetDefenseExtraRatioScaled(DamageType damageType) => defenseExtraRatioScaled[(int)damageType];
        public float GetDefensePerExtraBonus(DamageType damageType) => defensePerExtraBonus[(int)damageType];
        public float GetDefensePerExtraRatioUnscaled(DamageType damageType) => defensePerExtraRatioUnscaled[(int)damageType];
        public float GetDefensePerExtraRatioScaled(DamageType damageType) => defensePerExtraRatioScaled[(int)damageType];
        public float GetAmountExtraBonus(Attribute attribute) => amountExtraBonus[(int)attribute];
        public float GetAmountExtraRatioUnscaled(Attribute attribute) => amountExtraRatioUnscaled[(int)attribute];
        public float GetAmountExtraRatioScaled(Attribute attribute) => amountExtraRatioScaled[(int)attribute];
        public float GetDamageExtraRatio(DamageType damageType) => damageExtraRatio[(int)damageType];
 
    }

    public class EffectData
    {

    }

    [Serializable]
    public class LaunchMissileData : EffectData
    {
        [Header("- LaunchMissileStat")]
        [SerializeField] private float data21;
    }

    [Serializable]
    public class EnumAreaData : EffectData
    {
        [Header("- EnumAreaStat")]
        [SerializeField] private float data21;
    }

    [Serializable]
    public class DamageData : EffectData
    {
        [Header("- 피해타입")]
        [SerializeField] private DamageType type = DamageType.Physics;

        [Header("- 피해량")]
        [SerializeField] private float amount = 0.0f;

        [Header("- 특성피해 추가 보너스")]
        [Header(" 0:No, 1:Gu, 2:Ca, 3:Va, 4:Su, 5:Me, 6:De, 7:Sn, 8:Sp")]
        [SerializeField] private float[] amountExtraBonus = new float[(int)Attribute.End] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

        [Header("- 특성피해 추가 비율 조정안됨")]
        [Header(" 0:No, 1:Gu, 2:Ca, 3:Va, 4:Su, 5:Me, 6:De, 7:Sn, 8:Sp")]
        [SerializeField] private float[] amountExtraRatioUnscaled = new float[(int)Attribute.End] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

        [Header("- 특성피해 추가 비율 조정됨")]
        [Header(" 0:No, 1:Gu, 2:Ca, 3:Va, 4:Su, 5:Me, 6:De, 7:Sn, 8:Sp")]
        [SerializeField] private float[] amountExtraRatioScaled = new float[(int)Attribute.End] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

        [Header("- 방사피해사용")]
        [SerializeField] private bool isAOE = false;


        public float GetAmount() => amount;
        public DamageType GetDamageType() => type;
        public float GetAmountExtraBonus(Attribute attribute) => amountExtraBonus[(int)attribute];
        public float GetAmountExtraRatioUnscaled(Attribute attribute) => amountExtraRatioUnscaled[(int)attribute];
        public float GetAmountExtraRatioScaled(Attribute attribute) => amountExtraRatioScaled[(int)attribute];

        public bool IsAOE() => isAOE;
        public void SetAmount(float value) => amount = value;
        public void SetDamageType(DamageType value) => type = value;
        public void SetIsAOE(bool value) => isAOE = value;
        public void SetAmountExtraBonus(float value, Attribute attribute) => amountExtraBonus[(int)attribute] = value;
        public void SetAmountExtraRatioUnscaled(float value, Attribute attribute) => amountExtraRatioUnscaled[(int)attribute] = value;
        public void SetAmountExtraRatioScaled(float value, Attribute attribute) => amountExtraRatioScaled[(int)attribute] = value;
    }

    [Serializable]
    public class Range
    {
        [Header("- 영역타입")] 
        [SerializeField] private RangeType rangeType = RangeType.Grid;

        [Header("- 격자범위")] 
        [SerializeField] private List<Vector2Int> gridRange = new List<Vector2Int>();

        [Header("- 원형범위"), Range(0.0f, 10.0f)] 
        [SerializeField] private float circleRange = 0.0f;

        [Header("- 방향"), Range(0.0f, 360.0f)]
        [SerializeField] private float facingAdjustment = 0.0f;

        [Header("- 검색 갯수"), Range(1, 5)] //최대 몇명 까지 동시에 효과를 적용할지 결정한다
        [SerializeField] private uint multiSearch = 1;

        public RangeType GetRangeType() => rangeType;
        public List<Vector2Int> GetGridRange() => gridRange;
        public float GetCircleRange() => circleRange;
        public float GetFacingAdjustment() => facingAdjustment;
        public uint GetMultiSearch() => multiSearch;
        public void SetRangeType(RangeType value) => rangeType = value;
        public void SetCircleRange(float value) => circleRange = value;
        public void SetFacingAdjustment(float value) => facingAdjustment = value;
        public void SetMultiSearch(uint value) => multiSearch = value;
    }

    [Serializable]
    public class EnumAreaRange : Range
    {
        [Header("- 효과")]
        [SerializeField] private Effect effect;
    }
}
