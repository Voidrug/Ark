using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class UnitRoot : Root
    {
        [SerializeField, ReadOnly] private Unit unit;
        [SerializeField] private Unit unitPrefab;


        [Header("- 메타 데이터")]
        [SerializeField] private UnitRootMeta unitRootMeta;

        [Header("- 인게임 데이터")]
        [SerializeField, ReadOnly] private UnitRootData unitRootData;

        public Unit GetUnit() => unit;
        public Unit GetUnitPrefab() => unitPrefab;
        public override BaseMeta GetMeta() => unitRootMeta;
        public UnitRootData GetUnitRootData() => unitRootData;
        public override RootData GetData() => unitRootData;

        public override void CreateChild(Team team)
        {
            //팀 설정
            unitRootData.SetTeam(team);

            //자식 오브젝트 생성
            PoolManager poolManager = PoolManager.Inst();

            if(unitPrefab)
            {
                unit = (Unit)poolManager.GetPoolObj(unitPrefab.gameObject, this, team);
            }

            for (int i = 0; i < actorPrefabs.Count; i++)
            {
                AddActor((Actor)poolManager.GetPoolObj(actorPrefabs[i].gameObject, this, team));
            }
        }
        public override void StartEvent()
        {
            //이벤트 실행

            //하위
            unit.StartEvent();

            for (int i = 0; i < actors.Count; i++)
            {
                actors[i].StartEvent();
            }
        }
        public override void UpdateDataAll()
        {
            //업데이트 실행
            UpdateData();

            //하위
            unit.UpdateDataAll();
        }
        public override void UpdateData()
        {

        }

        public override void PreDestroy()
        {
            unit.PreDestroy();

            //하위 오브젝트 해제
            for (int i = 0; i < actors.Count; i++)
            {
                actors[i].PreDestroy();
            }
            DestroyManager.Inst().Destroy(this);
        }
        public override void LastDestroy()
        {
            if (gameObject.activeSelf == false)
                return;

            unit = null;
            actors.Clear();
            PoolManager.Inst().ReleaseObj(gameObject, unitRootMeta);
        }

        public override void EventStart(MessageType type, ScriptableObject metaData, string message, Root caster, Root creator, Root target)
        {
            for (int i = 0; i < actors.Count; i++)
            {
                actors[i].EventStart(type, metaData, message, caster, creator, target);
            }
        }
    }
}
