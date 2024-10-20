using Data;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SelectUI : UIBase
    {
        [Header("- UI View 데이터")]
        [SerializeField] private RectTransform leftSlider;
        [SerializeField] private RectTransform bottomSlider;
        [SerializeField] private GridLayoutGroup portraitLayout;
        [SerializeField] private VerticalLayoutGroup abilityInfoLayout;
/*        [SerializeField] private List<GameObject> charInfoPanel = new List<GameObject>();
        [SerializeField] private Image charInfoBack;*/

        [Header("- UI Sub ViewModel 데이터")]
        [SerializeField] private List<SelectPortraitUI> selectPortraitUis = new List<SelectPortraitUI>();
        [SerializeField] private List<GameObject> dummies = new List<GameObject>(); //마지막 빈공간 초상화
        [SerializeField] private SelectStatusInfoUI selectStatusInfoUI;
        [SerializeField] private List<SelectAbilityInfoUI> selectAbilityInfoUis = new List<SelectAbilityInfoUI>();

        [Header("- UI Field 데이터")]
        [SerializeField] private int selectedSelectPortraitIndex = -1;
        [SerializeField] private int selectedSelectAbilityIndex = -1;

        public void SetSelectedSelectPortraitIndex(int index) => selectedSelectPortraitIndex = index;
        public void SetSelectedSelectAbilityIndex(int index) => selectedSelectAbilityIndex = index;
        public SelectPortraitUI GetSelectedSelectPortraitUI() => selectedSelectPortraitIndex == -1 ? null : selectPortraitUis[selectedSelectPortraitIndex];
        public SelectAbilityInfoUI GetSelectedSelectAbilityUI() => selectedSelectAbilityIndex == -1 ? null : selectAbilityInfoUis[selectedSelectAbilityIndex];
        public int GetSelectedSelectPortraitIndex() => GetSelectedSelectPortraitUI() == null ? -1 : GetSelectedSelectPortraitUI().GetPortraitIndex();
        public int GetSelectedSelectAbilityIndex() => GetSelectedSelectAbilityUI() == null ? -1 : GetSelectedSelectAbilityUI().GetAbilityIndex();
        public List<SelectAbilityInfoUI> GetSelectAbilityInfoUIs() => selectAbilityInfoUis;
        public VerticalLayoutGroup GetAbilityInfoLayout() => abilityInfoLayout;

        void Awake()
        {
            UIManager.Inst().AddUI(UIEnum.Select, this);

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

            StartCoroutine(SliderUI(false, 0.25f));
        }
        public override void Init()
        {
            gameObject.SetActive(true);

            StartCoroutine(OpenCanvas(0.5f));

            StartCoroutine(SliderUI(true, 0.25f));

            // PortraitUI 갯수 설정
            PlayerManager playerManager = PlayerManager.Inst();
            List<UnitRoot> unitRoots = playerManager.GetUnitRoots();

            GameObject portraitPrefab = Resources.Load<GameObject>("UI/Lobby/Select/SelectPortraitPanel");
            GameObject dummyPrefab = Resources.Load<GameObject>("UI/Lobby/Select/Dummy");

            while (unitRoots.Count > selectPortraitUis.Count) //카드숫자대비 영웅이 더많으면 증가
            {
                GameObject portrait = Instantiate(portraitPrefab);
                portrait.transform.SetParent(portraitLayout.transform);
                portrait.transform.localPosition = new Vector3(portrait.transform.localPosition.x, portrait.transform.localPosition.y, 0);
                portrait.transform.localScale = new Vector3(1.25f, 1.25f, 1.0f);
                selectPortraitUis.Add(portrait.GetComponent<SelectPortraitUI>());
            }

            //하위 오브젝트 인덱스 초기화
            for (int i = 0; i < selectPortraitUis.Count; i++)
            {
                selectPortraitUis[i].SetPortraitIndex(i);
            }
            for (int i = 0; i < selectAbilityInfoUis.Count; i++)
            {
                selectAbilityInfoUis[i].SetAbilityIndex(i);
            }

            //플레이어 데이터를 통해서 선택된 PortaritUI 설정
            SquadUI squadUI = (SquadUI)UIManager.Inst().GetUI(UIEnum.Squad);
            SquadJson squadJson = playerManager.GetSquadJson();
            int selectedPortraitIndex = squadUI.GetSelectedPortraitIndex();
            int charIndex = squadJson.GetSquadCharNum(selectedPortraitIndex);
            SetSelectedSelectPortraitIndex(charIndex);

            //플레이어 데이터를 통해서 선택된 abilityinfoUI 설정
            SelectJson selectJson = playerManager.GetSelectJson();
            int abilityIndex = selectJson.GetSelectAbilityNum(charIndex);
            SetSelectedSelectAbilityIndex(abilityIndex);

            //select창 전용 능력 임시데이터 초기화
            selectJson.SetTempNums(playerManager.GetSquadTeamIndex());

            //PortraitUI 선택됨 전방배치
            if(GetSelectedSelectPortraitUI())
                GetSelectedSelectPortraitUI().transform.SetSiblingIndex(2); //2는 더미데이터 2개 뒤임

            //추가 후 더미 데이터 뒤에 끼워넣기 (앞은 미리 되어있음)
            for (int i = 0; i < dummies.Count; i++)
            {
                Destroy(dummies[i]);
            }   

            dummies.Clear();

            for (int i = 0; i < 2; i++)
            {
                GameObject dummy = Instantiate(dummyPrefab);
                dummy.transform.SetParent(portraitLayout.transform);

                dummies.Add(dummy);
            }

            //하위 오브젝트 초기화
            for (int i = 0; i < selectPortraitUis.Count; i++)
            {
                selectPortraitUis[i].Init();
            }
            for (int i =0; i < selectAbilityInfoUis.Count; i++)
            {
                selectAbilityInfoUis[i].Init();
            }
            {
                selectStatusInfoUI.Init();
            }
        }
        public override void UpdateDataAll()
        {
            UpdateData();
        }
        public override void UpdateData()
        {
            //하위 오브젝트 업데이트

            for (int i = 0; i < selectPortraitUis.Count; i++)
            {
                selectPortraitUis[i].UpdateDataAll();
            }
            for(int i = 0; i < selectAbilityInfoUis.Count; i++)
            {
                selectAbilityInfoUis[i].UpdateDataAll();
            }
            selectStatusInfoUI.UpdateDataAll();
        }

        public void OpenUIFinish()
        {
            //데이터 세이브
            SquadUI squadUI = (SquadUI)UIManager.Inst().GetUI(UIEnum.Squad);

            PlayerManager playerManager = PlayerManager.Inst();
            SquadJson squadJson = playerManager.GetSquadJson();
            SelectJson selectJson = playerManager.GetSelectJson();

            int selectPortraitIndex = this.GetSelectedSelectPortraitIndex();
            int squadPortraitIndex = squadUI.GetSelectedPortraitIndex();

            squadJson.SetSquadCharNum(selectPortraitIndex, squadPortraitIndex); //선택된 초상화넘버(unitroot),squad몇번째 초상화
            selectJson.SetSelectAbilityNum(selectJson.GetSelectTempNum(selectPortraitIndex), squadJson.GetSquadCharNum(squadPortraitIndex)); // 임시데이터 + 선택된 초상화넘버, squad몇번째 초상화

            for(int i = 0; i < squadJson.GetSquadCharNums().charNums.Length; i++) //중복 초상화 데이터 제거
            {
                if (squadJson.GetSquadCharNums().charNums[i] == selectPortraitIndex && i != squadPortraitIndex)
                {
                    squadJson.GetSquadCharNums().charNums[i] = -1;
                }
            }



            PlayerManager.Inst().SquadSave();
            PlayerManager.Inst().SelectSave();

            //창열기
            if(squadUI)
            {
                squadUI.OpenUI();
            }

            CloseUI();
        }

        public IEnumerator SliderUI(bool isOpen, float fullTime)
        {
            float time = 0.0f;

            Vector2 leftUIStart = new Vector2(100.0f, 0.0f);
            Vector2 leftUIEnd = new Vector2(245.0f, 0.0f);
            Vector2 bottomUIStart = new Vector2(700.0f, -37.5f);
            Vector2 bottomUIEnd = new Vector2(700.0f, 37.5f);

            while (time < fullTime)
            {
                time += Time.deltaTime;

                if (isOpen)
                {
                    leftSlider.anchoredPosition = Vector2.Lerp(leftUIStart, leftUIEnd, time / fullTime);
                    bottomSlider.anchoredPosition = Vector2.Lerp(bottomUIStart, bottomUIEnd, time / fullTime);
                }
                else
                {
                    leftSlider.anchoredPosition = Vector2.Lerp(leftUIEnd, leftUIStart, time / fullTime);
                    bottomSlider.anchoredPosition = Vector2.Lerp(bottomUIEnd, bottomUIStart, time / fullTime);
                }

                yield return null;
            }

            yield return null;
        }
    }
}
