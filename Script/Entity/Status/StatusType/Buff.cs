using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class Buff : Status
    {
        [Header("- 메타 데이터")]
        [SerializeField] private BuffMeta buffMeta;

        [Header("- 인게임 데이터")]
        [SerializeField, ReadOnly] private BuffData buffData;

        public override BaseMeta GetMeta() => buffMeta;
        public BuffMeta GetBuffMeta() => buffMeta;
        public BuffData GetData() => buffData;

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

        }
        public override void StartEvent()
        {

        }

        public override void PreDestroy()
        {
            DestroyManager.Inst().Destroy(this);
        }

        public override void LastDestroy()
        {
            if (gameObject.activeSelf == false)
                return;

            PoolManager.Inst().ReleaseObj(gameObject, buffMeta);
        }
    }
}
