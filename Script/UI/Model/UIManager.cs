using Player;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;
using UnityEngine;
using UltEvents;
using UnityEngine.EventSystems;
using Spine.Unity.Examples;

namespace UI
{
    public class UIManager : MonoSingle<UIManager>
    {
        private Dictionary<UIEnum, UIBase> uiDict = new Dictionary<UIEnum, UIBase>();

        private bool isUsing = false;

        private bool isPressed = false;

        public UIManager()
        {
            instance = this;
        }

        public void SetUsing(bool check) => isUsing = check;
        public bool IsUsing() => isUsing;

        public void AddUI(UIEnum uIEnum, UIBase uIBase)
        {
            if (!uiDict.ContainsKey(uIEnum))
            {
                uiDict.Add(uIEnum, uIBase);
            }
            else
            {
                uiDict[uIEnum] = uIBase;
            }
        }
        
        public UIBase GetUI(UIEnum uIEnum)
        {
            if (uiDict.ContainsKey(uIEnum))
            {
               return uiDict[uIEnum];
            }

            return null;
        }

        public void CloseUI(UIEnum uIEnum)
        {
            if (uiDict.ContainsKey(uIEnum))
            {
                uiDict[uIEnum].CloseUI();
            }
        }
    }
}
