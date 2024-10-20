using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;
using Spine.Unity;
using RotaryHeart.Lib.SerializableDictionary;
using static RotaryHeart.Lib.DataBaseExample;
using UltEvents;

namespace Data
{
    [Serializable]
    public class ActorMeta : BaseMeta
    {
        [Header("- 정적 데이터")]
        [SerializeField] protected Alias alias;

        [SerializeField] protected EventDict eventDict;

        [Header("- 동적 데이터")]
        [SerializeField] protected ActorData actorData;

        public Alias GetAlias() => alias;
        public EventDict GetEventDict() => eventDict;

        public void ActorLookAt(Root root, Actor actor, Vector3 vector) //root = 디폴트 출처 (아직 미사용), actor = 실행된 actor
        {
            actor.transform.localRotation = Quaternion.Euler(vector);
        }

        public void SetActor(Root root, Actor actor, bool active) //root = 디폴트 출처 (아직 미사용), actor = 실행된 actor
        {
            actor.gameObject.SetActive(active);
        }

        public void PlayAnimation(Root root, Actor actor, string animName) //root = 디폴트 출처 (아직 미사용), actor = 실행된 actor
        {
            Animator animator = actor.GetComponent<Animator>();
            animator.SetTrigger(animName);
        }
    }
}
