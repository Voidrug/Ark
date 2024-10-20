using Player;
using Level;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Data;

namespace UI
{
    public class MissionUI : UIBase
    {
        [Header("- UI View 데이터")]
        [SerializeField] private RectTransform content;
        [SerializeField] private Image background;

        [Header("- UI Sub ViewModel 데이터")]
        [SerializeField] private List<MissionButtonUI> missionButtons = new List<MissionButtonUI>();
        [SerializeField] private MissionPanelUI missionPanel;

        [Header("- UI 위치 데이터")]
        [SerializeField] private float maxPosition = 0.0f;
        [SerializeField] private float minPosition = -1000.0f;

        public MissionPanelUI GetMissionPanel() => missionPanel;

        void Awake()
        {
            //1회성 초기화
            UIManager.Inst().AddUI(UIEnum.Mission, this);

            this.gameObject.SetActive(false);
        }

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

            //하위 오브젝트 실행
            for(int i = 0; i < missionButtons.Count; i++)
            {
                missionButtons[i].Init();
            }

            missionPanel.Init();
            missionPanel.gameObject.SetActive(false);
        }
        public override void UpdateDataAll()
        {
            UpdateData();

            //하위 오브젝트 업데이트
            for (int i = 0; i < missionButtons.Count; i++)
            {
                missionButtons[i].UpdateDataAll();
            }

            missionPanel.UpdateDataAll();

        }
        public override void UpdateData()
        {
        }

        public void RestrictPosition()
        {
            if(content)
            {
                if(content.localPosition.x > maxPosition)
                {
                    content.localPosition = new Vector3(maxPosition, 0.0f, 0.0f);
                }

                if(content.localPosition.x < minPosition)
                {
                    content.localPosition = new Vector3(minPosition, 0.0f, 0.0f);
                }
            }

            if(background)
            {
                if (background.transform.localPosition.x > 0.0f)
                {
                    background.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                }

                if(background.transform.localPosition.x < -225.0f)
                {
                    background.transform.localPosition = new Vector3(-225.0f, 0.0f, 0.0f);
                }
            }
        }

        public void MoveBackground()
        {
            if (background && content)
            {
                float x = 225.0f / (maxPosition - minPosition) * content.localPosition.x - 960.0f;

                background.transform.localPosition = new Vector3(x, 0.0f, 0.0f);
            }
        }
        
        public void CloseUIBack()
        {
            LevelManager.Inst().SetMissionMeta(null);
            LevelManager.Inst().SetDungeonPrefab(null);

            CloseUI();

            LobbyUI lobbyUI = (LobbyUI)UIManager.Inst().GetUI(UIEnum.Lobby);

            lobbyUI.OpenUI();
        }
    }
}
