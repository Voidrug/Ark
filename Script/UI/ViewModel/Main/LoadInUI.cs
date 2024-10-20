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

namespace UI
{
    public class LoadInUI : UIBase
    {
        [Header("- UI View 데이터")]
        [SerializeField] private Text title;
        [SerializeField] private Text subTitle;

        void Awake()
        {
            //1회성 초기화
            UIManager.Inst().AddUI(UIEnum.LoadIn, this);

            this.gameObject.SetActive(false);
        }

        public override void OpenUI()
        {
            Init();

            UpdateData();
        }
        public override void CloseUI()
        {
            gameObject.SetActive(false);
        }
        public override void Init()
        {
            gameObject.SetActive(true);

            StartCoroutine(OpenCanvas(0.5f));
        }
        public override void UpdateDataAll()
        {
            UpdateData();
        }

        public override void UpdateData()
        {
            SquadUI squadUI = (SquadUI)UIManager.Inst().GetUI(UIEnum.Squad);
            MissionMeta missionMeta = LevelManager.Inst().GetMissionMeta();

            if (squadUI)
            {
                if(title)
                {
                    title.text = missionMeta.GetTitle();
                }

                if(subTitle)
                {
                    subTitle.text = missionMeta.GetSubTitle();
                }
            }
        }

    }
}
