using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class InstantAbil : Ability
    {
        [Header("- 메타 데이터")]
        [SerializeField] private InstantAbilMeta instantAbilMeta;

        [Header("- 인게임 데이터")]
        [SerializeField, ReadOnly] private InstantAbilData instantAbilData;

        public void SetMeta(InstantAbilMeta meta) => instantAbilMeta = meta;
        public override BaseMeta GetMeta() => instantAbilMeta;
        public InstantAbilMeta GetMoveMeta() => instantAbilMeta;

        public override void CreateChild(Team team)
        {
        }
        public override void UpdateDataAll()
        {
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

            transform.parent = null;

            PoolManager.Inst().ReleaseObj(gameObject, instantAbilMeta);
        }



    }


}

