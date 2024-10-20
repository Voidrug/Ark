using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;
using static UnityEngine.CompositeCollider2D;
using static UnityEngine.GraphicsBuffer;

namespace Data
{
    [Serializable]
    public class Damage : Effect
    {
        [Header("- 메타 데이터")]
        [SerializeField] private DamageMeta damageMeta;

        [Header("- 인게임 데이터")]
        [SerializeField, ReadOnly] private DamageData damageData;

        private List<Collider2D> rangeCol = new List<Collider2D>();
        public override BaseMeta GetMeta() => damageMeta;
        void Start()
        {
            //데이터 1회용 초기화(외부입력데이터)
            CreateRange();
        }
        public override void CreateChild(Team team)
        {
            //팀설정
            gameObject.layer = (int)team + 11;
        }
        public override void UpdateDataAll()
        {
            UpdateData();
        }
        public override void UpdateData()
        {
            //데이터 초기화
            DamageData data = damageMeta.GetDamageData();
            damageData.SetAmount(Calculations());
            damageData.SetDamageType(data.GetDamageType());
            damageData.SetIsAOE(data.IsAOE());

            for (int i = 0; i < (int)Attribute.End; i++)
            {
                damageData.SetAmountExtraBonus(data.GetAmountExtraBonus((Attribute)i), (Attribute)i);
                damageData.SetAmountExtraRatioScaled(data.GetAmountExtraRatioScaled((Attribute)i), (Attribute)i);
                damageData.SetAmountExtraRatioUnscaled(data.GetAmountExtraRatioUnscaled((Attribute)i), (Attribute)i);
            }

            isEnd = false;
        }
        public override void StartEvent()
        {

        }
        public override void PreDestroy()
        {
            CheckEnd();

            if (isEnd == false)
                return;

            //하위오브젝트가 먼저 풀로 돌아가야함 (큐구조)
            DestroyManager.Inst().Destroy(this);

            //부모 오브젝트 해제
            //Effect타입은 개별적으로 대기하다 파괴됨
            //기본타입은 하위오브젝트도 파괴하므로 확정적으로 파괴명령 수행함
            if (transform.parent.GetComponent<Effect>())
            {
                Effect parentEffect = transform.parent.GetComponent<Effect>();
                parentEffect.PreDestroy();
            }
            else if (transform.parent.GetComponent<EffectRoot>())
            {
                EffectRoot parentCreator = transform.parent.GetComponent<EffectRoot>();
                parentCreator.PreDestroy();
            }
        }
        public override void LastDestroy()
        {
            if (gameObject.activeSelf == false)
                return;

            transform.SetParent(null);

            PoolManager.Inst().ReleaseObj(gameObject, damageMeta);
        }
        private void CreateRange()
        {
            if(damageData.IsAOE())
            {
                if (damageMeta.GetRange().GetRangeType() == RangeType.Grid)
                {
                    List<Vector2Int> grid = damageMeta.GetRange().GetGridRange();

                    for (int i = 0; i < grid.Count; i++)
                    {
                        BoxCollider2D collider2D = gameObject.AddComponent<BoxCollider2D>();
                        collider2D.usedByComposite = true;
                        collider2D.offset = new Vector2(grid[i].x, grid[i].y);
                        collider2D.enabled = false;
                        rangeCol.Add(collider2D);
                    }

                    CompositeCollider2D com2D = gameObject.AddComponent<CompositeCollider2D>();
                    com2D.isTrigger = true;
                    com2D.geometryType = GeometryType.Polygons;
                }
                else if (damageMeta.GetRange().GetRangeType() == RangeType.Circle && damageMeta.GetRange().GetCircleRange() != 0.0f)
                {
                    Assert.AreNotEqual(0.0f, damageMeta.GetRange().GetCircleRange(), "원형 범위가 0이라 충돌체를 생성하지 않습니다." + damageMeta.name);
                    CircleCollider2D collider2D = gameObject.AddComponent<CircleCollider2D>();
                    collider2D.radius = damageMeta.GetRange().GetCircleRange();
                    collider2D.isTrigger = true;
                    collider2D.enabled = false;
                    rangeCol.Add(collider2D);
                }
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {            
            Unit unitCol = collision.GetComponent<Unit>();
            Assert.IsNotNull(unitCol, "유닛컴포넌트가 없는 대상(" + collision.name + ")이 감지됩니다");

            //피해전달
            unitCol.Damage(ReturnUpdateDamageValue(), damageData.GetDamageType());

            Debug.Log("b");
        }

        public float ReturnUpdateDamageValue()
        {
            float amount = Calculations();

            if (transform.parent.GetComponent<Unit>()) //툴팁용 데이터 일시
            {
                UnitData casterUnitData = transform.parent.GetComponent<Unit>().GetData();
                DamageType damageType = damageData.GetDamageType();
                Attribute attribute = Attribute.None;

                float ratioUnscaled = 1.0f;
                float bonus = 0.0f;
                float ratioScaled = 1.0f;
                float damageRatio = 1.0f;

                if(attribute != Attribute.None)
                {
                    ratioUnscaled += damageData.GetAmountExtraRatioUnscaled(attribute) + casterUnitData.GetAmountExtraRatioUnscaled(attribute);
                    bonus += damageData.GetAmountExtraBonus(attribute) + casterUnitData.GetAmountExtraBonus(attribute);
                    ratioScaled += damageData.GetAmountExtraRatioScaled(attribute) + casterUnitData.GetAmountExtraRatioScaled(attribute);
                    damageRatio += casterUnitData.GetDamageExtraRatio(damageType);
                }

                ratioUnscaled += damageData.GetAmountExtraRatioUnscaled(Attribute.None) + casterUnitData.GetAmountExtraRatioUnscaled(Attribute.None);
                bonus += damageData.GetAmountExtraBonus(Attribute.None) + casterUnitData.GetAmountExtraBonus(Attribute.None);
                ratioScaled += damageData.GetAmountExtraRatioScaled(Attribute.None) + casterUnitData.GetAmountExtraRatioScaled(Attribute.None);

                amount *= ratioUnscaled;
                amount += bonus;
                amount *= ratioScaled;
                amount *= damageRatio;
            }
            else
            {
                UnitData casterUnitData = effectLocation[(int)Location.Caster].GetComponent<UnitRoot>().GetUnit().GetData();
                Unit targetUnit = effectLocation[(int)Location.Target].GetComponent<UnitRoot>().GetUnit();
                DamageType damageType = damageData.GetDamageType();
                Attribute attribute = targetUnit.GetUnitMeta().GetAttribute();

                float ratioUnscaled = 1.0f;
                float bonus = 0.0f;
                float ratioScaled = 1.0f;
                float damageRatio = 1.0f;

                if (attribute != Attribute.None)
                {
                    ratioUnscaled += damageData.GetAmountExtraRatioUnscaled(attribute) + casterUnitData.GetAmountExtraRatioUnscaled(attribute);
                    bonus += damageData.GetAmountExtraBonus(attribute) + casterUnitData.GetAmountExtraBonus(attribute);
                    ratioScaled += damageData.GetAmountExtraRatioScaled(attribute) + casterUnitData.GetAmountExtraRatioScaled(attribute);
                    damageRatio += casterUnitData.GetDamageExtraRatio(damageType);
                }

                ratioUnscaled += damageData.GetAmountExtraRatioUnscaled(Attribute.None) + casterUnitData.GetAmountExtraRatioUnscaled(Attribute.None);
                bonus += damageData.GetAmountExtraBonus(Attribute.None) + casterUnitData.GetAmountExtraBonus(Attribute.None);
                ratioScaled += damageData.GetAmountExtraRatioScaled(Attribute.None) + casterUnitData.GetAmountExtraRatioScaled(Attribute.None);

                amount *= ratioUnscaled;
                amount += bonus;
                amount *= ratioScaled;
                amount *= damageRatio;
            }

            return amount;
        }

        protected override IEnumerator StartEffect()
        {
            transform.position = effectLocation[(int)damageMeta.GetImpactLocation()].position;

            Unit target = effectLocation[(int)Location.Target].GetComponent<UnitRoot>().GetUnit();            

            if (damageData.IsAOE() == true) // 범위피해
            {
                for(int i = 0; i < rangeCol.Count; i++)
                {
                    rangeCol[i].enabled = true;
                }
                yield return new WaitForSeconds(0.1f);
                for (int i = 0; i < rangeCol.Count; i++)
                {
                    rangeCol[i].enabled = false;
                }

                Debug.Log("c");
            }
            else 
            {
                //Assert.IsNotNull(target, "이미 죽은 대상에게 피해를 입히려고 합니다");

                if (target) //대상 사망여부 확인 (1인용 피해 수행여부)
                {
                    //피해전달
                    target.Damage(ReturnUpdateDamageValue(), damageData.GetDamageType());
                }
            }

            Debug.Log("a");

            PreDestroy();

            yield return null;
        }
        private float Calculations() //누산기
        {
            List<Accumulator> accumulators = damageMeta.GetAccumulators();

            float value = damageMeta.GetDamageData().GetAmount();

            for (int i = 0; i < accumulators.Count; i++)
            {
                value = Calculation(value, accumulators[i].GetAccumulatorMeta().Calculations(), accumulators[i].GetOperator());
            }

            return value;
        }
        private float Calculation(float value, float param, Operator oper)
        {
            if (oper == Operator.Add)
            {
                return value + param;
            }
            else if (oper == Operator.Multiply)
            {
                return value * param;
            }
            else if (oper == Operator.AdditiveMultiply)
            {
                return value + (param * value);
            }

            return value;
        }
    }
}
