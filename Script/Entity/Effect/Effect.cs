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
    public abstract class Effect : Base
    {
        [Header("- 효과 지역")]
        [SerializeField, ReadOnly] protected Transform[] effectLocation = new Transform[(int)Location.End];
        protected bool isEnd;
        protected abstract IEnumerator StartEffect();
        public bool IsEnd() => isEnd;

        public void EffectOrder(Transform caster, Transform creator, Transform target)
        {
            effectLocation[(int)Location.Caster] = caster;
            effectLocation[(int)Location.Creator] = creator;
            effectLocation[(int)Location.Target] = target;

            StartCoroutine(StartEffect());
        }
        public void CheckEnd()
        {
            bool Check = true;

            for(int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).GetComponent<Effect>().IsEnd() == false)
                {
                    Check = false;
                    break;
                }
            }

            isEnd = Check;
        }
    }
}
