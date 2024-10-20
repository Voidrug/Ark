using Data;
using Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.AddressableAssets.Build.Layout.BuildLayout;

namespace UI
{
    [Serializable]
    public class MissionButtonUI : UIBase
    {
        [Header("- 메타 데이터")]
        [SerializeField] private MissionMeta missionMeta;
        [SerializeField] private Dungeon dungeonPrefab;

        [Header("- UI View 데이터")]
        [SerializeField] private Text buttonTitle;
        [SerializeField] private Image border;

        public override void OpenUI()
        {
            Init();

            UpdateDataAll();
        }
        public override void CloseUI()
        {
            gameObject.SetActive(false);
        }
        public override void Init()
        {
            gameObject.SetActive(true);

            if (missionMeta)
            {
                if (buttonTitle)
                {
                    buttonTitle.text = missionMeta.GetTitle();
                }
            }

            if(border)
            {
                border.gameObject.SetActive(false);
            }
        }
        public override void UpdateDataAll()
        {
            UpdateData();
        }
        public override void UpdateData()
        {
            MissionMeta managerMeta = LevelManager.Inst().GetMissionMeta();

            if (border)
            {
                if (missionMeta == managerMeta)
                {
                    border.gameObject.SetActive(true);
                }
                else
                {
                    border.gameObject.SetActive(false);
                }
            }
        }

        public void OpenUIPanel()
        {
            LevelManager.Inst().SetMissionMeta(missionMeta);
            LevelManager.Inst().SetDungeonPrefab(dungeonPrefab);

            MissionUI missionUI = (MissionUI)UIManager.Inst().GetUI(UIEnum.Mission);
            missionUI.GetMissionPanel().OpenUI();
        }
    }
}
