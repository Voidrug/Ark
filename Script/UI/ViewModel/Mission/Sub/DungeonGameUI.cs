using Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEditor.AddressableAssets.Build.Layout.BuildLayout;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Build.Pipeline.Tasks;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SocialPlatforms;
using Data;
using Player;
using System.Collections;


namespace UI
{
    public class DungeonGameUI : UIBase
    {
        [Header("- UI View 데이터")]
        [SerializeField] private RectTransform pausePanel;
        [SerializeField] private Button pause;
        [SerializeField] private Button setting;
        [SerializeField] private Button play;
        [SerializeField] private Button oneX;
        [SerializeField] private Button twoX;
        [SerializeField] private Text enemyMax;
        [SerializeField] private Text enemyCount;
        [SerializeField] private Text lifeCount;
        [SerializeField] private Text cost;
        [SerializeField] private Image costProgress;
        [SerializeField] private Text battleCount;

        [SerializeField] private HorizontalLayoutGroup heroPortraitLayout;

        [Header("- UI Field 데이터")]
        [SerializeField] private int selectedHeroPortraitIndex = -1;
        [SerializeField] private int selectedHeroPortraitPrevIndex = -1;

        [Header("- UI Sub ViewModel 데이터")]
        [SerializeField] private List<HeroPortraitUI> heroPortraitUis;

        private UnitRoot cursorUnit;
        private float timeScale;


        public void SetSelectedHeroPortraitIndex(int index)
        {
            selectedHeroPortraitPrevIndex = selectedHeroPortraitIndex;
            selectedHeroPortraitIndex = index;
        }
        public int GetSelectedHeroPortraitIndex() => selectedHeroPortraitIndex;
        public int GetSelectedHeroPortraitPrevIndex() => selectedHeroPortraitPrevIndex;
        public HeroPortraitUI GetSelectedHeroPortraitUI() => selectedHeroPortraitIndex == -1 ? null : heroPortraitUis[selectedHeroPortraitIndex];
        public HeroPortraitUI GetSelectedHeroPortraitPrevUI() => selectedHeroPortraitPrevIndex == -1 ? null : heroPortraitUis[selectedHeroPortraitPrevIndex];
        public float GetTimeScale() => timeScale;

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


            //패널 초기화
            MissionMeta missionMeta = LevelManager.Inst().GetMissionMeta();

            if (pausePanel)
            {
                pausePanel.gameObject.SetActive(false);
            }

            if(pause)
            {
                pause.gameObject.SetActive(true);
            }

            if(play)
            {
                play.gameObject.SetActive(false);
            }

            if(oneX)
            {
                oneX.gameObject.SetActive(true);
            }

            if(twoX)
            {
                twoX.gameObject.SetActive(false);
            }

            timeScale = 1.0f;

            // HeroPortraitUI 갯수 설정
            GameManager gameManager = GameManager.Inst();
            List<UnitRoot> unitRoots = gameManager.GetUnitRoots();

            GameObject portraitPrefab = Resources.Load<GameObject>("UI/Mission/Dungeon/Hero");

            while (unitRoots.Count > heroPortraitUis.Count) //카드숫자대비 영웅이 더많으면 증가
            {
                GameObject portrait = Instantiate(portraitPrefab);
                portrait.transform.SetParent(heroPortraitLayout.transform);
                portrait.transform.localPosition = new Vector3(portrait.transform.localPosition.x, portrait.transform.localPosition.y, 0);
                portrait.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                heroPortraitUis.Add(portrait.GetComponent<HeroPortraitUI>());
            }

            //하위 오브젝트 인덱스 초기화
            for (int i = 0; i < heroPortraitUis.Count; i++)
            {
                heroPortraitUis[i].SetPortraitIndex(i);
            }


            //하위 오브젝트 초기화

            for (int i = 0; i < heroPortraitUis.Count; i++)
            {
                heroPortraitUis[i].Init();
            }

        }

        private void Update()
        {
            //데이터 업데이트
            Dungeon dungeon = LevelManager.Inst().GetDungeon();

            if (dungeon == null)
                return;
     
            DungeonData dungeonData = dungeon.GetDungeonData();
            float prevCost = dungeonData.GetCost();
            float nextCost = prevCost + dungeonData.GetCostRegen() * Time.deltaTime;

            dungeonData.SetCost(nextCost);

            if (cost)
            {
                cost.text = ((int)nextCost).ToString();
            }

            if (costProgress)
            {
                costProgress.fillAmount = nextCost - (int)nextCost;
            }
        }
        public override void UpdateDataAll()
        {
            UpdateData();

            //하위 오브젝트 업데이트

            for (int i = 0; i < heroPortraitUis.Count; i++)
            {
                heroPortraitUis[i].UpdateDataAll();
            }
        }

        public override void UpdateData()
        {
            if (gameObject.activeSelf == false)
                return;

            //데이터 업데이트

            MissionMeta missionMeta = LevelManager.Inst().GetMissionMeta();
            Dungeon dungeon = LevelManager.Inst().GetDungeon();
            DungeonMeta dungeonMeta = dungeon.GetDungeonMeta();
            DungeonData dungeonData = dungeon.GetDungeonData();

            if(dungeon != null)
            {
                if (enemyMax)
                {
                    enemyMax.text = dungeonMeta.GetMonsterSpawns().Count.ToString();
                }

                if (enemyCount)
                {
                    enemyCount.text = dungeon.GetEnemyCount().ToString();
                }

                if (lifeCount)
                {
                    lifeCount.text = dungeonData.GetLifeCount().ToString();
                }

                if (cost)
                {
                    cost.text = ((int)dungeonData.GetCost()).ToString();
                }

                if (costProgress)
                {
                    costProgress.fillAmount = dungeonData.GetCost() - (int)dungeonData.GetCost();
                }

                if (battleCount)
                {
                    battleCount.text = dungeonData.GetBattleCount().ToString();
                }
            }
        }

        public void Pause()
        {
            if(pausePanel)
            {
                pausePanel.gameObject.SetActive(true);
            }

            if(pause)
            {
                pause.gameObject.SetActive(false);
            }

            if(play)
            {
                play.gameObject.SetActive(true);
            }

            Time.timeScale = 0.0f;
        }

        public void Play()
        {
            if (pausePanel)
            {
                pausePanel.gameObject.SetActive(false);
            }

            if (pause)
            {
                pause.gameObject.SetActive(true);
            }

            if (play)
            {
                play.gameObject.SetActive(false);
            }

            Time.timeScale = timeScale;
        }

        public void OneX()
        {
            if (oneX)
            {
                oneX.gameObject.SetActive(false);
            }

            if (twoX)
            {
                twoX.gameObject.SetActive(true);
            }

            timeScale = 2.0f;

            if (!pausePanel.gameObject.activeSelf)
            {
                Time.timeScale = timeScale;
            }           
        }

        public void TwoX()
        {
            if (oneX)
            {
                oneX.gameObject.SetActive(true);
            }

            if (twoX)
            {
                twoX.gameObject.SetActive(false);
            }

            timeScale = 1.0f;

            if (!pausePanel.gameObject.activeSelf)
            {
                Time.timeScale = timeScale;
            }
        }

        public void Setting()
        {
            DungeonUI dungeonUI = (DungeonUI)UIManager.Inst().GetUI(UIEnum.Dungeon);

            dungeonUI.GetDungeonGameUI().CloseUI();

            dungeonUI.GetDungeonStopUI().OpenUI();

            //카메라 설정
            Camera mainCam = LevelManager.Inst().GetMainCamera();

            mainCam.GetComponent<PostProcessVolume>().enabled = true;

            //게임정지
            Time.timeScale = 0.0f;
        }
    }
}
