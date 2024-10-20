using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class TargetAbil : Ability
    {
        [Header("- 메타 데이터")]
        [SerializeField] private TargetAbilMeta targetAbilMeta;

        [Header("- 인게임 데이터")]
        [SerializeField, ReadOnly] private TargetAbilData targetAbilData;

        public void SetMeta(TargetAbilMeta meta) => targetAbilMeta = meta;
        public override BaseMeta GetMeta() => targetAbilMeta;
        public TargetAbilMeta GetTargetAbilMeta() => targetAbilMeta;

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
            
            PoolManager.Inst().ReleaseObj(gameObject, targetAbilMeta);
        }



    }


}
