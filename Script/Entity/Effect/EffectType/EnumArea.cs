using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;
using System.Collections;

namespace Data
{
    [Serializable]
    public class EnumArea : Effect
    {
        [Header("- 자식 데이터")]


        [Header("- 원본 데이터")]
        [SerializeField] private LaunchMissileMeta effectMeta;

        [Header("- 인게임 데이터")]
        [SerializeField, ReadOnly] private LaunchMissileData effectData;

        public override BaseMeta GetMeta() => effectMeta;

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
            isEnd = false;
        }
        public override void StartEvent()
        {

        }
        public override void PreDestroy()
        {


        }
        public override void LastDestroy()
        {

        }

        protected override IEnumerator StartEffect()
        {


            yield return null;
        }
    }
}
