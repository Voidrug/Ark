using Data;
using Level;
using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DungeonUI : UIBase
    {
        [Header("- UI Sub ViewModel 데이터")]
        [SerializeField] private HeroInfoUI heroInfoUI;
        [SerializeField] private DungeonGameUI dungeonGameUI;
        [SerializeField] private DungeonStopUI dungeonStopUI;
        [SerializeField] private DungeonEndUI dungeonEndUI;
        [SerializeField] private DungeonFailUI dungeonFailUI;

        public HeroInfoUI GetHeroInfoUI() => heroInfoUI;
        public DungeonGameUI GetDungeonGameUI() => dungeonGameUI;
        public DungeonStopUI GetDungeonStopUI() => dungeonStopUI;
        void Awake()
        {
            //1회성 초기화
            UIManager.Inst().AddUI(UIEnum.Dungeon, this);
        }

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

/*            // PortraitUI 갯수 설정
            List<UnitRoot> unitRoots = PlayerManager.Inst().GetPlayerData().GetCharData().GetUnitRoots();

            GameObject portraitPrefab = Resources.Load<GameObject>("UI/Portrait/SelectPortraitPanel");

            while (unitRoots.Count > heroPortraitUis.Count)
            {
                GameObject portrait = Instantiate(portraitPrefab);
                portrait.transform.SetParent(portraitLayout.transform);
                portrait.transform.localPosition = new Vector3(portrait.transform.localPosition.x, portrait.transform.localPosition.y, 0);
                portrait.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                heroPortraitUis.Add(portrait.GetComponent<HeroPortraitUI>());
            }

            SquadUI squadUI = (SquadUI)UIManager.Inst().GetUI(UIEnum.Squad);

            for (int i = 0; i < selectPortraitUis.Count; i++)
            {
                if (unitRoots.Count > i)
                {
                    selectPortraitUis[i].gameObject.SetActive(true);
                }
                else
                {
                    selectPortraitUis[i].gameObject.SetActive(false);
                }
            }

            //하위 오브젝트 인덱스 초기화

            for (int i = 0; i < heroPortraitUis.Count; i++)
            {
                heroPortraitUis[i].SetPortraitIndex(i);
            }*/

            //하위 오브젝트 초기화

            if (dungeonGameUI)
            {
                dungeonGameUI.Init();
            }
            if (dungeonStopUI)
            {
                dungeonStopUI.gameObject.SetActive(false);
            }
            if (dungeonEndUI)
            {
                dungeonEndUI.gameObject.SetActive(false);
            }
            if (dungeonFailUI)
            {
                dungeonFailUI.gameObject.SetActive(false);
            }
        }
        
        public override void UpdateDataAll()
        {
            UpdateData();

            //하위 오브젝트 업데이트

            if (heroInfoUI)
            {
                heroInfoUI.UpdateDataAll();
            }
            if (dungeonGameUI)
            {
                dungeonGameUI.UpdateDataAll();
            }
            if (dungeonStopUI)
            {
                dungeonStopUI.UpdateDataAll();
            }
            if (dungeonEndUI)
            {
                dungeonEndUI.UpdateDataAll();
            }
            if (dungeonFailUI)
            {
                dungeonFailUI.UpdateDataAll();
            }

        }

        public override void UpdateData()
        {
            //데이터 업데이트

            if (gameObject.activeSelf == false)
                return;
        }

    }
}
