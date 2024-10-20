using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

namespace Data
{
    //Actor를 생성되기 전에는 이벤트를 입력이 불가능 하기 때문에 Actor생성 action의 경우 해당 매니저에서 처리함
    public class CreatorManager : MonoSingle<CreatorManager>
    {
        [Header("- 메타 데이터")]
        [SerializeField] private CreatorMeta creatorMeta;

        void Awake()
        {
            instance = this;
        }

        public void EventStart(MessageType messageType, ScriptableObject metaData, string message, Root caster, Root creator, Root target)
        {
            EventKey eventKey = new EventKey(messageType,metaData,message);

            EventDict eventDict = creatorMeta.GetEventDict();

            if (eventDict == null)
                return;

            if (!eventDict.ContainsKey(eventKey))
                return;

            //조건 검사
            List<Condition> conditions = eventDict[eventKey].GetConditions();

            Root root = null; //디폴트 대상

            for (int i = 0; i < conditions.Count; i++)
            {
                ConditionType conditionType = conditions[i].GetConditionType();

                if (conditionType == ConditionType.Location)
                {
                    if(conditions[i].GetLocation() == Location.Caster)
                    {
                        root = caster;
                    }
                    else if(conditions[i].GetLocation() == Location.Creator)
                    {
                        root = creator;
                    }
                    else if(conditions[i].GetLocation() == Location.Target)
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
            eventDict[eventKey].GetActions().Invoke(root, null); //null은 메타데이터에 전달시 자기자신 actor를 입력하므로 여기는 안쓰임
        }
    }
}
