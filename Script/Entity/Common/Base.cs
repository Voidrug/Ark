using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    [Serializable]
    public abstract class Base :MonoBehaviour
    {
        [Header("- 부모 데이터")]
        [SerializeField, ReadOnly] protected Root rootBase;

        public void SetRoot(Root value) => rootBase = value;
        public Root GetRoot() => rootBase;

        public virtual void Init()
        {
            if (!GetMeta())
            {
                transform.name = "None";
                return;
            }
            else
            {
                transform.name = GetMeta().name;
            }

/*            LinkChild();
            UpdateDataAll();*/
        }

        public abstract void CreateChild(Team team);
        public abstract void UpdateData();
        public abstract void UpdateDataAll();
        public abstract void StartEvent();
        public abstract void PreDestroy();
        public abstract void LastDestroy();
        public abstract BaseMeta GetMeta();        
    }
}
