using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;
using System.Collections;
using System.ComponentModel.Design.Serialization;


namespace Data
{
    [Serializable]
    public class LaunchMissile : Effect
    {
        [Header("- 자식 데이터")]
        [SerializeField, ReadOnly] private Effect launchEffect;
        [SerializeField] private Effect launchPrefab;

        [SerializeField, ReadOnly] private Effect impactEffect;
        [SerializeField] private Effect impactPrefab;

        [SerializeField] private UnitRoot missilePrefab;
        [SerializeField] private GameObject missileMover;

        [Header("- 메타 데이터")]
        [SerializeField] private LaunchMissileMeta launchMissileMeta;

        [Header("- 인게임 데이터")]
        [SerializeField, ReadOnly] private LaunchMissileData launchMissileData;

        public Effect GetLaunchPrefab() => launchPrefab;
        public Effect GetImpactPrefab() => impactPrefab;
        public UnitRoot GetMissilePrefab() => missilePrefab;
        public override BaseMeta GetMeta() => launchMissileMeta;

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
            else if(transform.parent.GetComponent<EffectRoot>())
            {
                EffectRoot parentRoot = transform.parent.GetComponent<EffectRoot>();
                parentRoot.PreDestroy();
            }
        }
        public override void LastDestroy()
        {
            if (gameObject.activeSelf == false)
                return;

            transform.SetParent(null);
            launchEffect = null;
            impactEffect = null;            

            PoolManager.Inst().ReleaseObj(gameObject, launchMissileMeta);
        }

        protected override IEnumerator StartEffect()
        {
            //미사일 생성
            Team team = ((EffectRoot)rootBase).GetEffectRootData().GetTeam();
            Base missile;

            if (missilePrefab)
            {
                missile = PoolManager.Inst().GetObj(missilePrefab.gameObject, null, team);
                missile.transform.position = effectLocation[(int)launchMissileMeta.GetLaunchLocation()].position;
                missile.GetComponent<UnitRoot>().Init();
            }
            else
            {
                GameObject prefab = DataManager.Inst().GetPrefab("Default");
                missile = PoolManager.Inst().GetObj(prefab, null, team);
            }

            //발사 효과 생성
            if (launchPrefab)
            {
                launchEffect = PoolManager.Inst().GetObj(launchPrefab.gameObject, this, team).GetComponent<Effect>();
                launchEffect.transform.parent = transform;
                launchEffect.EffectOrder(effectLocation[(int)Location.Caster], missile.transform, effectLocation[(int)Location.Target]);
                launchEffect.Init();
            }

            Transform targetTransform = effectLocation[(int)Location.Target];
            Transform missileTransform = missile.GetRoot().transform;

            //미사일 최초 방향 설정 (플래그)
            if ((launchMissileMeta.GetFlag() & LMFlag.CasterDir) == LMFlag.CasterDir)
            {
                missileTransform.position = effectLocation[(int)launchMissileMeta.GetLaunchLocation()].transform.position;
                missileTransform.rotation = effectLocation[(int)launchMissileMeta.GetLaunchLocation()].transform.rotation;
            }

            //타게팅 대상 생존 여부
            bool live = true;
            Vector3 TargetPosition = targetTransform.position;

            //타게팅 대상 지점일때 업데이트 방지
            if (launchMissileMeta.IsImpactLand() == true)
                live = false;

            //미사일 이동
            float missileSpeed = missile.GetComponent<UnitRoot>().GetUnit().GetData().GetMoveSpeed();
            while (Vector3.Distance(TargetPosition, missile.transform.position) > 0.1f)
            {
                
                if (targetTransform.gameObject.activeSelf == true && live)
                {
                    TargetPosition = targetTransform.position;
                }
                else
                {
                    //대상이 제거되면 미사일 타게팅 업데이트 중단
                    live = false;
                }

                float step = missileSpeed * Time.deltaTime;
                missileTransform.position = Vector3.MoveTowards(missileTransform.position, TargetPosition, step);

                yield return null;
            }

            //충격 효과 생성
            if (impactPrefab && live)
            {
                impactEffect = PoolManager.Inst().GetObj(impactPrefab.gameObject, this, team).GetComponent<Effect>();
                impactEffect.transform.parent = transform;
                impactEffect.EffectOrder(effectLocation[(int)Location.Caster], missile.transform, effectLocation[(int)Location.Target]);
                impactEffect.Init();
            }


            missile.GetComponent<UnitRoot>().PreDestroy();

            PreDestroy();

            yield return null;
        }
    }
}
