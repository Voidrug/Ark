using Player;
using Level;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoadOutUI : UIBase
    {
        [Header("- UI View 데이터")]
        [SerializeField] private CanvasGroup background;
        [SerializeField] private CanvasGroup textPanel;
        [SerializeField] private Text title;
        [SerializeField] private Text subTitle;

        [Header("- 맵 데이터")]
        [SerializeField] private Transform map;

        void Awake()
        {
            //1회성 초기화
            UIManager.Inst().AddUI(UIEnum.LoadOut, this);

            this.gameObject.SetActive(false);
        }

        public override void OpenUI()
        {
            Init();

            UpdateDataAll();
        }
        public override void CloseUI()
        {
            StartCoroutine(CloseCanvasStep(0.5f,0.5f));
        }
        public override void Init()
        {
            gameObject.SetActive(true);
        }
        public override void UpdateDataAll()
        {
            UpdateData();
        }
        public override void UpdateData()
        {
            if (gameObject.activeSelf == false)
                return;

            SquadUI squadUI = (SquadUI)UIManager.Inst().GetUI(UIEnum.Squad);
            MissionMeta missionMeta = LevelManager.Inst().GetMissionMeta();

            if (squadUI)
            {
                if (title)
                {
                    title.text = missionMeta.GetTitle();
                }

                if (subTitle)
                {
                    subTitle.text = missionMeta.GetSubTitle();
                }
            }
        }

        public virtual IEnumerator CloseCanvasStep(float ImageTime, float panelTime)
        {
            //이미지 비활성화

            float time = 0.0f;

            while (time < ImageTime)
            {
                time += Time.deltaTime;

                background.alpha = Mathf.Lerp(1.0f, 0.0f, time / ImageTime);

                yield return null;
            }

            yield return new WaitForSeconds(1.0f);

            //텍스트 비활성화

            time = 0.0f;

            while (time < panelTime)
            {
                time += Time.deltaTime;

                textPanel.alpha = Mathf.Lerp(1.0f, 0.0f, time / panelTime);

                yield return null;
            }

            gameObject.SetActive(false);
            UIManager.Inst().SetUsing(false);

            yield return null;
        }

    }
}

