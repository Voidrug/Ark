using Spine.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    [Serializable]
    [CreateAssetMenu(fileName = "ModelData", menuName = "Base/Actor/ModelMeta")]
    public class ModelMeta : ActorMeta
    {
        [Header("- 동적 데이터")]
        [SerializeField] private ModelData modelData;

        public ModelData GetModelData() => modelData;

        public void ModelFilp(Root root, Actor actor, bool check) //root = 대상, actor = 실행된 actor
        {
            SkeletonMecanim skeletonMecanim = actor.GetComponent<SkeletonMecanim>();

            if (skeletonMecanim == null)
                return;

            if (check)
            {
                skeletonMecanim.skeleton.ScaleX = -1;
            }
            else
            {
                skeletonMecanim.skeleton.ScaleX = 1;
            }
        }
    }
}
