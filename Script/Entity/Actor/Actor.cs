using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;
using UnityEditor;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static Spine.Unity.Examples.SpineboyFootplanter;

namespace Data
{
    [Serializable]
    public abstract class Actor : Base
    {
        [Header("- 자식 데이터")]
        [SerializeField, ReadOnly] protected List<Actor> actors = new List<Actor>();

        public void AddActor(Actor actor) => actors.Add(actor);
        public abstract ActorMeta GetActorMeta();

        public override void CreateChild(Team team)
        {
        }
        public override void UpdateDataAll()
        {
            //업데이트 실행
            UpdateData();

            //하위
            for (int i = 0; i < actors.Count; i++)
            {
                actors[i].UpdateDataAll();
            }
        }
        public override void StartEvent()
        {
            //이벤트 실행
            ActorCreate();

            //하위
            for (int i = 0; i < actors.Count; i++)
            {
                actors[i].StartEvent();
            }
        }
        public Actor FindSameActor(ActorMeta actorMeta, Alias alias)
        {
            Actor target = null;

            for (int i = 0; i < actors.Count; i++)
            {
                ActorMeta meta = actors[i].GetActorMeta();

                if (meta == actorMeta || (meta.GetAlias() & alias) > 0) //so가 같거나 alias가 동일해야함(none)빼고                    
                {
                    target = actors[i];

                    if (target)
                        return target;
                }
            }

            for (int i = 0; i < actors.Count; i++)
            {
                target = actors[i].FindSameActor(actorMeta, alias);
            }

            return target;
        }

        public T GetActor<T>() where T : Actor
        {
            T component = null;

            for (int i = 0; i < actors.Count; i++)
            {
                component = actors[i].GetComponent<T>();

                if (component)
                {
                    return component;
                }
            }

            for (int i = 0; i < actors.Count; i++)
            {
                component = actors[i].GetActor<T>();

                if (component)
                {
                    return component;
                }
            }

            return component;
        }

        public void GetActors<T>(List<T> inout) where T : Actor
        {
            T component = null;

            for (int i = 0; i < actors.Count; i++)
            {
                component = actors[i].GetComponent<T>();

                if (component)
                {
                    inout.Add(component);
                }
            }

            for (int i = 0; i < actors.Count; i++)
            {
                actors[i].GetActors<T>(inout);
            }

        }

        public void EventStart(MessageType messageType, ScriptableObject metaData, string message, Root caster, Root creator, Root target)
        {
            EventKey eventKey = new EventKey(messageType, metaData, message);

            EventDict eventDict = GetActorMeta().GetEventDict();

            if (eventDict == null)
            {
                return;
            }                

            if (!eventDict.ContainsKey(eventKey))
            {
                return;
            }

            Debug.Log(eventKey.GetMessageType());
            Debug.Log(eventKey.GetMetaData());
            Debug.Log(eventKey.GetMessage());


            //조건 검사
            List<Condition> conditions = eventDict[eventKey].GetConditions();

            Root root = null; //디폴트 출처 //아직 미사용

            for (int i = 0; i < conditions.Count; i++)
            {
                ConditionType conditionType = conditions[i].GetConditionType();

                if (conditionType == ConditionType.Location)
                {
                    if (conditions[i].GetLocation() == Location.Caster)
                    {
                        root = caster;
                    }
                    else if (conditions[i].GetLocation() == Location.Creator)
                    {
                        root = creator;
                    }
                    else if (conditions[i].GetLocation() == Location.Target)
                    {
                        root = target;
                    }
                }
                else if (conditionType == ConditionType.AnimName)
                {
                    if (eventKey.GetMessage() != conditions[i].GetStr())
                    {
                        return;
                    }
                }
            }

            //이벤트 전달
            eventDict[eventKey].GetActions().Invoke(root, this); 


            //하위메뉴 이벤트 시작
            for (int i = 0; i < actors.Count; i++)
            {
                actors[i].EventStart(messageType, metaData, message, caster, creator, target);
            }
        }
        public void ActorCreate()
        {
            CreatorManager.Inst().EventStart(MessageType.ActorCreate, GetActorMeta(), "", rootBase, rootBase, rootBase);
            rootBase.EventStart(MessageType.ActorCreate, GetActorMeta(), "", rootBase, rootBase, rootBase);
        }
    }
}
