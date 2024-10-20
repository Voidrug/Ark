using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;


namespace Data
{
    [Serializable]
    public class BaseMeta : ScriptableObject
    {
        [Header("- ID")]
        [SerializeField] protected string id;

        public void SetID(string value) => id = value;
        public string GetID() => id;

        private void OnEnable()
        {
            if (string.IsNullOrEmpty(id))
            {
                id = Guid.NewGuid().ToString();
            }
        }
    }

}