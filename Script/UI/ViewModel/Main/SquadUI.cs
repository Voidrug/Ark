using Data;
using Player;
using Level;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace UI
{
    [Serializable]
    public class SquadUI : UIBase
    {
        [Header("- UI Sub ViewModel 데이터")]
        [SerializeField] private RectTransform supportPortrait;
        [SerializeField] private RectTransform startPortrait;
        [SerializeField] private List<SquadPortraitUI> squadPortraitUis = new List<SquadPortraitUI>();
        [SerializeField] private List<SquadTeamUI> squadTeamUis = new List<SquadTeamUI>();

        [Header("- UI Field 데이터")]
        [SerializeField] private int selectedSquadPortraitIndex = -1;
        [SerializeField] private int selectedSquadTeamIndex = 0;


        public void SetSelectedPoratraitIndex(int index) => selectedSquadPortraitIndex = index;
        public void SetSelectedSquadTeamIndex(int index) => selectedSquadTeamIndex = index;
        public SquadPortraitUI GetSelectedPoratraitUI() => selectedSquadPortraitIndex == -1 ? null : squadPortraitUis[selectedSquadPortraitIndex];
        public SquadTeamUI GetSelectedSquadTeamUI() => selectedSquadTeamIndex == -1 ? null : squadTeamUis[selectedSquadTeamIndex];
        public int GetSelectedPortraitIndex() => GetSelectedPoratraitUI() == null ? -1 : GetSelectedPoratraitUI().GetPortraitIndex();
        public int GetSelectedSquadTeamIndex() => GetSelectedSquadTeamUI() == null ? -1 : GetSelectedSquadTeamUI().GetTeamIndex();



        void Awake()
        {
            //매니저에 등록
            UIManager.Inst().AddUI(UIEnum.Squad, this);

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
            gameObject.SetActive(true);

            StartCoroutine(OpenCanvas(0.5f));

            //하위 오브젝트 인덱스 초기화

            for (int i = 0; i < squadTeamUis.Count; i++)
            {
                squadTeamUis[i].SetTeamIndex(i);
            }
            for (int i = 0; i < squadPortraitUis.Count; i++)
            {
                squadPortraitUis[i].SetPortraitIndex(i);
            }

            //선택된 ui 설정
            PlayerManager playerManager = PlayerManager.Inst();
            SquadJson squadJson = playerManager.GetSquadJson();

            SetSelectedPoratraitIndex(-1);

            //하위 UI 오픈여부설정

            for (int i = 0; i < squadTeamUis.Count; i++)
            {
                squadTeamUis[i].Init();
            }

            for (int i = 0; i < squadPortraitUis.Count; i++)
            {
                squadPortraitUis[i].Init();
            }

        }

        public override void UpdateDataAll()
        {
            UpdateData();

            //하위 오브젝트 업데이트
            for (int i = 0; i < squadTeamUis.Count; i++)
            {
                squadTeamUis[i].UpdateDataAll();
            }

            for (int i = 0; i < squadPortraitUis.Count; i++)
            {
                squadPortraitUis[i].UpdateDataAll();
            }
        }

        public override void UpdateData()
        {
            //스쿼드 데이터 로드
            PlayerManager playerManager = PlayerManager.Inst();
            SquadJson squadJson = playerManager.GetSquadJson();
            MissionMeta missionMeta = LevelManager.Inst().GetMissionMeta();

            //미션진입전 스쿼드인지 확인
            if(missionMeta && supportPortrait && startPortrait)
            {
                supportPortrait.gameObject.SetActive(true);
                startPortrait.gameObject.SetActive(true);
            }
            else
            {
                supportPortrait.gameObject.SetActive(false);
                startPortrait.gameObject.SetActive(false);
            }
        }

        public void CloseUIBack()
        {
            CloseUI();

            MissionUI missionUI = (MissionUI)UIManager.Inst().GetUI(UIEnum.Mission);
            LobbyUI lobbyUI = (LobbyUI)UIManager.Inst().GetUI(UIEnum.Lobby);
            MissionMeta missionMeta = LevelManager.Inst().GetMissionMeta();

            if (missionMeta)
            {
                missionMeta = null;

                missionUI.OpenUI();
            }
            else
            {
                lobbyUI.OpenUI();
            }
        }

        public void OpenUIStart()
        {
            MissionUI missionUI = (MissionUI)UIManager.Inst().GetUI(UIEnum.Mission);
            MissionMeta missionMeta = LevelManager.Inst().GetMissionMeta();

            CloseUI();

            if (missionMeta)
            {
                LevelManager.Inst().LoadScene(missionMeta.GetSceneName());
            }
        }
    }
}
