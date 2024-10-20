using Data;
using Level;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class HeroPortraitUI : UIBase
    {
        [Header("- UI View 데이터")]
        [SerializeField] private Image portrait;
        [SerializeField] private Image top;
        [SerializeField] private Image attribute;
        [SerializeField] private Text cost;
        [SerializeField] private Button button;

        [Header("- UI Field 데이터")]
        [SerializeField] private int portraitIndex = -1;

        public int SetPortraitIndex(int index) => portraitIndex = index;

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
        }
        public override void UpdateDataAll()
        {
            UpdateData();
        }

        public override void UpdateData()
        {
            if (gameObject.activeSelf == false)
                return;

            GameManager gameManager = GameManager.Inst();
            List<UnitRoot> unitRoots = gameManager.GetUnitRoots();

            UnitRoot root = unitRoots[portraitIndex];
            Unit unit = root?.GetUnit();
            List<Ability> abilities = unit?.GetAbilityList();
            UnitMeta unitMeta = unit?.GetUnitMeta();
            UnitData unitData = unit?.GetData();
            Hero hero = unit?.GetStatus<Hero>();
            HeroData heroData = hero?.GetData();
            HeroMeta heroMeta = hero?.GetHeroMeta();
            Data.Attribute heroAttribute = unitMeta.GetAttribute();

            // 버튼 슬라이더 설정
            DungeonUI dungeonUI = (DungeonUI)UIManager.Inst().GetUI(UIEnum.Dungeon);
            DungeonGameUI dungeonGameUI = dungeonUI.GetDungeonGameUI();

            if (dungeonGameUI.GetSelectedHeroPortraitUI() != dungeonGameUI.GetSelectedHeroPortraitPrevUI())//이전과 이후가 다르면
            {
                if(dungeonGameUI.GetSelectedHeroPortraitUI() == this)
                {
                    StartCoroutine(ButtonSliderUp(0.1f));
                }

                if(dungeonGameUI.GetSelectedHeroPortraitPrevUI() == this)
                {
                    StartCoroutine(ButtonSliderDown(0.1f));
                }                
            }

            // 데이터 설정

            if (portrait)
            {
                portrait.sprite = heroMeta.GetBlockImage();
            }

            if(top)
            {
                if(heroAttribute == Data.Attribute.Vanguard ||
                    heroAttribute == Data.Attribute.Guard ||
                    heroAttribute == Data.Attribute.Defender)
                {
                    top.color = new Color(255f / 255f, 232f / 255f, 0f / 255f, 255f / 255f);
                }
                else if(heroAttribute == Data.Attribute.Sniper ||
                    heroAttribute == Data.Attribute.Caster)
                {
                    top.color = new Color(255f / 255f, 56f / 255f, 0f / 255f, 255f / 255f);
                }
                else if(heroAttribute == Data.Attribute.Medic ||
                    heroAttribute == Data.Attribute.Supporter)
                {
                    top.color = new Color(255f / 255f, 184f / 255f, 255f / 255f, 255f / 255f);
                }
                else if(heroAttribute == Data.Attribute.Specialist)
                {
                    top.color = new Color(0f / 255f, 184f / 255f, 255f / 255f, 255f / 255f);
                }
            }

            if(attribute)
            {
                attribute.sprite = heroMeta.GetAttributeSmallImage();
            }

            if(cost)
            {
                cost.text = unitData.GetCost().ToString();
            }
        }

        public void PointerClickButton()
        {
            //영웅 정보창 열기
            UIManager uiManager = UIManager.Inst();
            DungeonUI dungeonUI = (DungeonUI)uiManager.GetUI(UIEnum.Dungeon);
            HeroInfoUI heroInfoUI = dungeonUI.GetHeroInfoUI();
            DungeonGameUI dungeonGameUI = dungeonUI.GetDungeonGameUI();

            if(heroInfoUI.gameObject.activeSelf == false)
            {
                dungeonGameUI.SetSelectedHeroPortraitIndex(portraitIndex);
                heroInfoUI.OpenUI();
            }
            else
            {
                if(dungeonGameUI.GetSelectedHeroPortraitUI() == this)//기존에 선택된 초상화가 자기 자신이면 영운 info 닫기
                {
                    dungeonGameUI.SetSelectedHeroPortraitIndex(-1);

                    heroInfoUI.CloseUI();
                }
                else //다른 초상화를 선택했을 경우 데이터 업데이트
                {
                    dungeonGameUI.SetSelectedHeroPortraitIndex(portraitIndex);
                    heroInfoUI.UpdateDataAll();
                }
            }

            dungeonGameUI.UpdateDataAll();
        }
        public void BeginDragButton()
        {
            //영웅 정보창 열기
            UIManager uiManager = UIManager.Inst();
            DungeonUI dungeonUI = (DungeonUI)uiManager.GetUI(UIEnum.Dungeon);
            HeroInfoUI heroInfoUI = dungeonUI.GetHeroInfoUI();
            DungeonGameUI dungeonGameUI = dungeonUI.GetDungeonGameUI();

            if (heroInfoUI.gameObject.activeSelf == false)
            {
                dungeonGameUI.SetSelectedHeroPortraitIndex(portraitIndex);
                heroInfoUI.OpenUI();
            }
            else
            {
                if (dungeonGameUI.GetSelectedHeroPortraitUI() != this)//기존에 선택된 초상화가 자기 자신이면 영운 info 닫기
                {
                    dungeonGameUI.SetSelectedHeroPortraitIndex(portraitIndex);
                    heroInfoUI.UpdateDataAll();
                }
            }

            StartCoroutine(dungeonUI.GetHeroInfoUI().InfoSlider(0.2f));

            StartCoroutine(EndDragButton());

            dungeonGameUI.UpdateDataAll();
        }

        public IEnumerator EndDragButton()
        {
            UIManager uiManager = UIManager.Inst();
            GameManager gameManager = GameManager.Inst();
            LevelManager levelManager = LevelManager.Inst();

            DungeonUI dungeonUI = (DungeonUI)uiManager.GetUI(UIEnum.Dungeon);
            PosUI posUI = (PosUI)uiManager.GetUI(UIEnum.Pos);
            HeroInfoUI heroInfoUI = dungeonUI.GetHeroInfoUI();
            DungeonGameUI dungeonGameUI = dungeonUI.GetDungeonGameUI();
            UnitRoot unitRoot = gameManager.GetUnitRoots()[dungeonGameUI.GetSelectedHeroPortraitIndex()];
            unitRoot.gameObject.SetActive(true);
            unitRoot.EventStart(MessageType.Signal, null, "Aoe.Off", unitRoot, unitRoot, unitRoot); //Aoe 비활성화
            unitRoot.EventStart(MessageType.Signal, null, "Front", unitRoot, unitRoot, unitRoot); //앞면 활성화,뒷면 비활성화
            Transform unitTransform = unitRoot.transform;
            MeshRenderer meshRenderer = null;
            int prevLayer = 0;  
            List<Actor> actors = new List<Actor>();
            unitRoot.GetActors<Actor>(actors);

            //Model Actor 레이어 변경으로 최상단에 위치시킴
            for (int i = 0; i < actors.Count; i++) 
            {
                if (actors[i].GetComponent<MeshRenderer>())
                {
                    meshRenderer = actors[i].GetComponent<MeshRenderer>();
                    prevLayer = meshRenderer.gameObject.layer;
                    meshRenderer.gameObject.layer = 31; 
                    break;
                }
            }

            //특정 배치 타일 블럭 활성화
            Hero hero = unitRoot.GetUnit().GetStatus<Hero>();

            if(hero)
            {
                levelManager.SetTileColor(hero.GetHeroMeta(), true);
            }

            bool check = true;
            Plane plane = new Plane(Vector3.forward, Vector3.zero); //마우스 raycast용 원점 plane
            float distance;
            RaycastHit hit;

            while (check)
            {
                //오브젝트 충돌 계산
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                bool createCheck = false;

                if (Physics.Raycast(ray, out hit)) 
                {
                    Vector3 centor = hit.collider.GetComponent<BoxCollider>().center;
                    centor.z = 0.0f;

                    unitTransform.localPosition = centor; //충돌체에 고정

                    unitRoot.EventStart(MessageType.Signal, null, "Aoe.On", unitRoot, unitRoot, unitRoot);

                    createCheck = true; //생성 여부 체크
                }
                else if(plane.Raycast(ray, out distance))// 평면 충돌 계산
                {
                    unitTransform.localPosition = ray.GetPoint(distance); //마우스 커서 따라 이동

                    unitRoot.EventStart(MessageType.Signal, null, "Aoe.Off", unitRoot, unitRoot, unitRoot);

                    createCheck = false; //생성 여부 체크
                }

                if (Input.GetMouseButtonUp(0))
                {         
                    yield return new WaitUntil(() => Input.GetMouseButtonUp(0));

                    //마우스 드래그 떼기
                    levelManager.SetTileColor(hero.GetHeroMeta(), false); //타일원상복귀
                    check = false;

                    if(createCheck == true)//생성 여부 체크
                    {
                        posUI.OpenUI();
                    }
                    else
                    {
                        heroInfoUI.CloseUI();
                        dungeonGameUI.SetSelectedHeroPortraitIndex(-1);
                        unitRoot.gameObject.SetActive(false);
                    }


                    dungeonGameUI.UpdateDataAll();
                }

                yield return null;
            }

            yield return null;
        }

        public IEnumerator ButtonSliderUp(float fullTime)
        {
            float time = 0.0f;

            while (time < fullTime)
            {
                time += Time.unscaledDeltaTime;

                transform.GetComponent<RectTransform>().sizeDelta = new Vector2(186.0f, Mathf.Lerp(193.0f, 215.0f, time / fullTime));

                yield return null;
            }

            yield return null;
        }

        public IEnumerator ButtonSliderDown(float fullTime)
        {
            float time = 0.0f;

            while (time < fullTime)
            {
                time += Time.unscaledDeltaTime;

                transform.GetComponent<RectTransform>().sizeDelta = new Vector2(186.0f, Mathf.Lerp(215.0f, 193.0f, time / fullTime));

                yield return null;
            }

            yield return null;
        }



    }
}
