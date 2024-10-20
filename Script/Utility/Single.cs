using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{    
    public abstract class Single<T> where T : Single<T>, new()
    {
        private static T instance;
        public static T Inst()
        {
            if (instance == null)
            {
                instance = new T();

                instance.Initialization();
            }           

            return instance;
        }

        public static void Destroy()
        {
            instance = null;
        }

        public virtual void Initialization() { }
    }

    public abstract class MonoSingle<T> : MonoBehaviour where T : MonoSingle<T>
    {
        protected static T instance;

        public static T Inst()
        {
            if(instance == null)
            {
                GameObject gameObject = new GameObject(typeof(T).Name);
                instance = gameObject.AddComponent<T>();
                //DontDestroyOnLoad(gameObject);

                instance.Initialization();
            }            

            return instance;           
        }

        public virtual void Initialization() { }
    }
}
