using Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

namespace UI
{
    public class DungeonStopUI : UIBase
    {
        [Header("- UI View 데이터")]
        [SerializeField] private Button returnGame;
        [SerializeField] private Button giveUp;

        public override void OpenUI()
        {
            Init();

            UpdateDataAll();
        }
        public override void CloseUI()
        {
            StartCoroutine(CloseCanvas(0.25f));
        }
        public override void Init()
        {
            gameObject.SetActive(true);

            StartCoroutine(OpenCanvas(0.25f));
        }
        public override void UpdateDataAll()
        {
            UpdateData();
        }
        public override void UpdateData()
        {
            if (gameObject.activeSelf == false)
                return;
        }
        public void ReturnGame()
        {
            DungeonUI dungeonUI = (DungeonUI)UIManager.Inst().GetUI(UIEnum.Dungeon);

            dungeonUI.GetDungeonStopUI().CloseUI();

            DungeonGameUI dungeonGameUI = dungeonUI.GetDungeonGameUI();

            dungeonGameUI.gameObject.SetActive(true);

            //카메라 설정
            Camera mainCam = LevelManager.Inst().GetMainCamera();

            mainCam.GetComponent<PostProcessVolume>().enabled = false;

            //게임정지
            Time.timeScale = dungeonGameUI.GetTimeScale();
        }

    }
}
