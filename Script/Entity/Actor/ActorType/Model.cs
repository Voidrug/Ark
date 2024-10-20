using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class Model : Actor
    {
        [Header("- 원본 데이터")]
        [SerializeField] private ModelMeta modelMeta;

        [Header("- 인게임 데이터")]
        [SerializeField, ReadOnly] private ModelData modelData;
        public override BaseMeta GetMeta() => modelMeta;
        public override ActorMeta GetActorMeta() => modelMeta;

        public override void UpdateData()
        {

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

            transform.parent = null;
            actors.Clear();
            PoolManager.Inst().ReleaseObj(gameObject, modelMeta);
        }
    }
}
