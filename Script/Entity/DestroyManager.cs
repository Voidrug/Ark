using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;
using UnityEngine;

namespace Data
{
    public class DestroyManager : MonoSingle<DestroyManager>
    {
        private Queue<Data.Base> baseQueue = new Queue<Data.Base>();


        private void LateUpdate()
        {
            while (baseQueue.Count > 0)
            {
                baseQueue.Dequeue().LastDestroy();
            }
        }

        public void Destroy(Data.Base baseObj)
        {
            baseQueue.Enqueue(baseObj);
        }


    }
}

