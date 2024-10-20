using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

namespace Data
{
    [Serializable]
    public class EffectRoot : Root
    {
        [SerializeField, ReadOnly] private Effect effect;
        [SerializeField] private Effect effectPrefab;

        [Header("- 메타 데이터")]
        [SerializeField] private EffectRootMeta effectRootMeta;

        [Header("- 인게임 데이터")]
        [SerializeField, ReadOnly] private EffectRootData effectRootData;

        public EffectRootData GetEffectRootData() => effectRootData;
        public override RootData GetData() => effectRootData;

        public Effect GetEffectPrefab() => effectPrefab;
        public override BaseMeta GetMeta() => effectRootMeta;

        public override void CreateChild(Team team)
        {
            //팀 설정
            effectRootData.SetTeam(team);

            //자식 오브젝트 생성
            PoolManager poolManager = PoolManager.Inst();

            if (effectPrefab)
            {
                effect = (Effect)poolManager.GetPoolObj(effectPrefab.gameObject, this, team);
            }

            for (int i = 0; i < actorPrefabs.Count; i++)
            {
                AddActor((Actor)poolManager.GetPoolObj(actorPrefabs[i].gameObject, this, team));
            }
        }
        public override void StartEvent()
        {
            effect.StartEvent();

            for (int i = 0; i < actors.Count; i++)
            {
                actors[i].StartEvent();
            }
        }
        public override void UpdateDataAll()
        {
            UpdateData();
        }
        public override void UpdateData()
        {
            //추가 데이터 적용
        }

        public override void PreDestroy()
        {
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

            effect = null;
            actors.Clear();
            PoolManager.Inst().ReleaseObj(gameObject, effectRootMeta);
        }


        public void EffectOrder(Transform caster, Transform create, Transform target)
        {
            effect.EffectOrder(caster, create, target);
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
