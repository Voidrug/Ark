using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI;
using UnityEngine;

namespace Level
{
    [Serializable]
    public class MainCam : MonoBehaviour
    {
        void Awake() //씬로드시 씬의 카메라에서 등록 시작함
        {
            //1회성 초기화
            Camera camera = gameObject.GetComponent<Camera>();

            if(camera)
            {
                LevelManager.Inst().SetMainCamera(camera);

                //자식 오브젝트에서 unitCam 등록
                for(int i = 0; i< camera.transform.childCount; i++)
                {
                    Camera unitCam = camera.transform.GetChild(i).GetComponent<Camera>();

                    if (unitCam)
                    {
                        LevelManager.Inst().SetUnitCamera(unitCam);
                    }
                }        
            } 
        }
    }

}
