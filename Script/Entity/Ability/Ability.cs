using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;

namespace Data
{
    [Serializable]
    public abstract class Ability : Base
    {
        void Start()
        {
            //데이터 1회용 초기화(외부입력데이터)
            CreateRange();
        }

        private void CreateRange()
        {
            /*            if (!abilityMeta)
                            return;

                        for (int i = 0; i < abilityMeta.GetRridRange().Count; i++)
                        {
                            BoxCollider2D collider2D = gameObject.AddComponent<BoxCollider2D>();
                            collider2D.usedByComposite = true;


                            collider2D.offset = new Vector2(abilityMeta.GetRridRange()[i].x, abilityMeta.GetRridRange()[i].y);
                        }

                        Rigidbody2D rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
                        CompositeCollider2D composite2D = gameObject.AddComponent<CompositeCollider2D>();

                        rigidbody2D.simulated = false;*/
        }
    }


}
