using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Build.Pipeline;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Data
{
    [Serializable]
    public abstract class Root : Base
    {
        [Header("- 자식 데이터")]
        [SerializeField, ReadOnly] protected List<Actor> actors = new List<Actor>();
        [SerializeField] protected List<Actor> actorPrefabs = new List<Actor>();



        public List<Actor> GetActorPrefabs() => actorPrefabs;
        public void AddActor(Actor actor) => actors.Add(actor);
        public abstract void EventStart(MessageType messageType, ScriptableObject metaData, string message, Root caster, Root creator, Root target);
        public abstract RootData GetData();

        private void Awake()
        {
            Init();
        }

        public Actor FindSameActor(ActorMeta actorMeta, Alias alias)
        {
            //Actor를 우선적으로 검색
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
    }
}
