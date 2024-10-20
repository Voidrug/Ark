using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using Tools;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Data
{  
    public class DataManager : MonoSingle<DataManager>
    {
        private Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>();

        private Dictionary<string, ScriptableObject> soDict = new Dictionary<string, ScriptableObject>();

        [SerializeField] private List<UnitRoot> charListRoot = new List<UnitRoot>();

        public GameObject GetPrefab(string name) => prefabDict.ContainsKey(name) ? prefabDict[name] : LoadPrefab(name);
        public ScriptableObject GetScriptableObject(string path) => soDict.ContainsKey(path) ? soDict[path] : LoadScriptableObject(path);
        public List<UnitRoot> GetCharListRoot() => charListRoot;


        public DataManager()
        {
            instance = this;
        }

        private GameObject LoadPrefab(string name)
        {
            GameObject prefab = Resources.Load<GameObject>(name);
            prefab.SetActive(false);
            prefabDict.Add(name, prefab);
            return prefab;
        }
        private ScriptableObject LoadScriptableObject(string path)
        {
            ScriptableObject so = Resources.Load<ScriptableObject>(path);
            soDict.Add(path, so);
            return so;
        }
        public GameObject GetInstObject(UnitRoot prefab)
        {
            GameObject instantObj = Instantiate(prefab.gameObject);

            return instantObj;
        }
    }


}
