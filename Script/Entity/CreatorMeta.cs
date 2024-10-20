using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    [Serializable]
    [CreateAssetMenu(fileName = "CreatorData", menuName = "Base/CreatorMeta")]
    public class CreatorMeta : ScriptableObject
    {
        [Header("-생성 이벤트 모음")]
        [SerializeField] private EventDict eventDict;

        public EventDict GetEventDict() => eventDict;

        public void ActorCreateAction(Actor actorPrefab, Root targetParent, Transform changeTransform, ActorMeta sameActor, Alias sameAlias, bool isParent)
        {
            //Actor : 생성할 프리펩
            //Root : 생성할 부모대상
            //Transform : 생성시 방향 설정
            //ActorMeta : Loaction작동시 연결할 Actor 대상찾기
            //Bool : 연결 여부

            //생성할 프리팹 보유 여부/대상 지정자 보유여부
            if (actorPrefab == null || targetParent == null)
            {
                Debug.LogWarning("생성하려는 Actor가 null입니다.");
                return;
            }

            //생성
            Actor actor = PoolManager.Inst().GetObj(actorPrefab.gameObject, null, targetParent.GetData().GetTeam()).GetComponent<Actor>();

            //위치변환
            if (changeTransform != null)
            {
                actor.transform.localPosition = changeTransform.localPosition;
                actor.transform.localRotation = changeTransform.localRotation;
                actor.transform.localScale = changeTransform.localScale;
            }

            //별도로 찾아서 부모 설정해줌
            if (targetParent != null)
            {
                Actor findActor = targetParent.FindSameActor(sameActor, sameAlias);

                if (findActor)
                {
                    if (isParent == false)
                    {
                        findActor.AddActor(actor);

                        actor.transform.SetParent(findActor.transform);
                    }
                }
            }
            else
            {
                actor.transform.localPosition = targetParent.transform.localPosition;
            }
        }
    }

}
