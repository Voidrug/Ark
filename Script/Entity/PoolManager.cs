using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Pool;
using UnityEngine.UIElements;
using static UnityEditor.Progress;



namespace Data
{
    public class PoolManager : MonoSingle<PoolManager>
    {
        GameObject prefab;

        private Dictionary<string, IObjectPool<GameObject>> rootDict = new Dictionary<string, IObjectPool<GameObject>>();

        void Awake()
        {
            rootDict = new Dictionary<string, IObjectPool<GameObject>>();
        }

        public Base GetObj(GameObject basePrefab, Base parent, Team team)
        {
            Base obj = GetPoolObj(basePrefab, parent, team);

            obj.UpdateDataAll();

            return obj;
        }
        public Base GetPoolObj(GameObject basePrefab, Base parent, Team team)
        {
            Assert.IsNotNull(basePrefab, "존재하지 않는 프리팹을 생성하려고 합니다");
            BaseMeta baseMeta = basePrefab.GetComponent<Base>().GetMeta();
            Assert.IsNotNull(baseMeta, "메타데이터가 없는 프리팹을 생성하려고 합니다 ->" + basePrefab.name);

            prefab = basePrefab;
            string name = baseMeta.GetType().Name + "/" + baseMeta.name;

            if (!rootDict.ContainsKey(name))
            {
                IObjectPool<GameObject> pool = new ObjectPool<GameObject>(CreateObj, OnGetObj, OnReleaseObj, OnDestroyObj, maxSize: 100);
                rootDict.Add(name, pool);
            }

            GameObject poolObj = rootDict[name].Get();
            Base objBase = poolObj.GetComponent<Base>();
            poolObj.SetActive(true);

            if (parent) //부모가 존재하면 부모의루트데이터를 자신의 루트데이터로 받아오고 부모에 연결
            {
                objBase.SetRoot(parent.GetRoot());
                poolObj.transform.SetParent(parent.transform);
            }
            else //부모가 없다면 Root계열이기 때문에 자기 자신을 형변환해서 전달함
            {
                objBase.SetRoot((Root)objBase);
            }
            //자식 오브젝트 생성명령 전달
            objBase.CreateChild(team);

            return objBase;
        }

        public void ReleaseObj(GameObject obj, BaseMeta metaData)
        {
            Assert.IsNotNull(obj, "존재하지 않는 프리팹을 지우려고 합니다 -> CreatorObj");
            Assert.IsNotNull(metaData, "메타데이터가 없는 프리팹을 불러오려고 합니다 ->" + obj.name);

            string name = metaData.GetType().Name + "/" + metaData.name;

            rootDict[name].Release(obj);
        }

        private GameObject CreateObj()
        {
            GameObject poolObj = Instantiate(prefab);
            return poolObj;
        }

        private void OnGetObj(GameObject obj)
        {
            obj.transform.SetParent(null);
        }

        private void OnReleaseObj(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.SetParent(transform);
        }
        private void OnDestroyObj(GameObject obj)
        {
            GameObject.Destroy(obj);
        }

    }

}


