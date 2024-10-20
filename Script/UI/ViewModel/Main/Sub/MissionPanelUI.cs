using Data;
using Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class MissionPanelUI : UIBase
    {
        [Header("- UI View 데이터")]
        [SerializeField] private Text title;
        [SerializeField] private Text subTitle;
        [SerializeField] private Text info;
        [SerializeField] private Button cancle;

        public override void OpenUI()
        {
            Init();

            UpdateDataAll();
        }

        public override void CloseUI()
        {
            StartCoroutine(CloseCanvas(0.5f));
        }
        public override void Init()
        {
            //캔버스 열기
            gameObject.SetActive(true);

            StartCoroutine(OpenCanvas(0.5f));
        }
        public override void UpdateDataAll()
        {
            UpdateData();
        }
        public override void UpdateData()
        {
            MissionUI missionUI = (MissionUI)UIManager.Inst().GetUI(UIEnum.Mission);

            MissionMeta missionMeta = LevelManager.Inst().GetMissionMeta();

            if (missionMeta)
            {
                gameObject.SetActive(true);

                if (title)
                {
                    title.text = missionMeta.GetTitle();
                }
                if(subTitle)
                {
                    subTitle.text = missionMeta.GetSubTitle();
                }
                if(info)
                {
                    info.text = missionMeta.GetInfo();
                }
            }
        }

        public void CloseUIPanel()
        {
            CloseUI();

            LevelManager.Inst().SetMissionMeta(null);
            LevelManager.Inst().SetDungeonPrefab(null);

            MissionUI missionUI = (MissionUI)UIManager.Inst().GetUI(UIEnum.Mission);           

            missionUI.UpdateDataAll();
        }

        public void OpenUISquad()
        {
            SquadUI squadUI = (SquadUI)UIManager.Inst().GetUI(UIEnum.Squad);
            squadUI.OpenUI();

            MissionUI missionUI = (MissionUI)UIManager.Inst().GetUI(UIEnum.Mission);
            missionUI.CloseUI();
        }

    }
}
