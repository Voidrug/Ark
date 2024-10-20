using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    [Serializable]
    [CreateAssetMenu(fileName = "MissionData", menuName = "Level/MissionMeta")]
    public class MissionMeta : ScriptableObject
    {
        [Header("- 정적 데이터")]

        [Header("- 씬 이름")]
        [SerializeField] private string sceneName;

        [Header("- 미션 제목")]
        [SerializeField] private string title;

        [Header("- 미션 부제목")]
        [SerializeField] private string subTitle;

        [Header("- 미션 정보")]
        [TextArea(3, 10)]
        [SerializeField] private string info;

        public string GetSceneName() => sceneName;
        public string GetTitle() => title;
        public string GetSubTitle() => subTitle;
        public string GetInfo() => info;
    }
}

