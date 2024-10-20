using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Level
{
    [Serializable]
    public struct Path
    {
        [SerializeField] private string abilityName;
        [SerializeField] private float waitTime;
        [SerializeField] private Vector2 targetPos;
    }

    [Serializable]
    [CreateAssetMenu(fileName = "PathOrderData", menuName = "Level/PathOrderMeta")]
    public class PathOrderMeta : ScriptableObject
    {
        [Header("- 씬 이름")]
        [SerializeField] private List<Path> paths;

        public List<Path> GetPaths() => paths;
    }
}
