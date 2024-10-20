using Data;
using Level;
using Player;
using Spine.Unity;
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
    public class PosUI : UIBase
    {
        [Header("- UI View 데이터")]
        [SerializeField] private Image hole;
        [SerializeField] private RectTransform holeClone;
        [SerializeField] private Image info;
        [SerializeField] private Image cancle;
        [SerializeField] private Image up;
        [SerializeField] private Image down;    
        [SerializeField] private Image right;
        [SerializeField] private Image left;
        [SerializeField] private Image move;
        [SerializeField] private Button borderButton;

        [SerializeField] private Vector3 mousePos;

        private Coroutine dragCoroutine;

        void Awake()
        {
            //1회성 초기화
            UIManager.Inst().AddUI(UIEnum.Pos, this);

            this.gameObject.SetActive(false);
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

            //유닛 위치로 이동

            DungeonUI dungeonUI = (DungeonUI)UIManager.Inst().GetUI(UIEnum.Dungeon);
            int heroNum = dungeonUI.GetDungeonGameUI().GetSelectedHeroPortraitIndex();
            UnitRoot selectedUnit = GameManager.Inst().GetUnitRoot(heroNum);
            Camera maincam = LevelManager.Inst().GetMainCamera();


            Vector3 position = selectedUnit.transform.localPosition; //이미지 위치 정확하게 하기위한 임시유닛위치 재조정
            position.y += 0.5f;

            Vector3 screenPosition = maincam.WorldToScreenPoint(position); //유닛위치를 카메라함수를 이용해 스크린좌표로 반환
            screenPosition.x -= Screen.width / 2.0f;
            screenPosition.y -= Screen.height / 2.0f;


            //ui 설정
            if (hole)
            {
                hole.rectTransform.localPosition = screenPosition;
            }
            if (holeClone)
            {
                holeClone.localPosition = screenPosition;
            }
            if (move)
            {
                move.rectTransform.localPosition = screenPosition;
            }
            if (cancle)
            {
                cancle.gameObject.SetActive(true);
            }
            if (info)
            {
                info.gameObject.SetActive(false);
            }
            if (up && down && left && right)
            {
                up.gameObject.SetActive(false);
                down.gameObject.SetActive(false);
                left.gameObject.SetActive(false);
                right.gameObject.SetActive(false);
            }
        }

        public override void UpdateDataAll()
        {
            UpdateData();
        }

        public override void UpdateData()
        {
            if (gameObject.activeSelf == false)
                return;

            //데이터 업데이트

        }

        public void BeginDragButton()
        {
            //드래그 시작 
            dragCoroutine = StartCoroutine(DragCursorMove());
        }

        public void EndDragButton()
        {
            DungeonUI dungeonUI = (DungeonUI)UIManager.Inst().GetUI(UIEnum.Dungeon);
            HeroInfoUI heroInfoUI = dungeonUI.GetHeroInfoUI();
            DungeonGameUI dungeonGameUI = dungeonUI.GetDungeonGameUI();
            Root hero = GameManager.Inst().GetUnitRoot(dungeonGameUI.GetSelectedHeroPortraitIndex());

            //드래그 종료 
            StopCoroutine(dragCoroutine);

            if(info.gameObject.activeSelf == true) //배치 성공
            {
                heroInfoUI.CloseUI();

                CloseUI();

                dungeonGameUI.GetSelectedHeroPortraitUI().gameObject.SetActive(false);

                hero.EventStart(MessageType.Signal, null, "SummonStart", hero, hero, hero);
                hero.EventStart(MessageType.Signal, null, "Dir.Aoe.On", hero, hero, hero);
            }
            else if(cancle.gameObject.activeSelf == true) //배치 보류
            {
                Init();
            }

            hero.EventStart(MessageType.Signal, null, "Aoe.Off", hero, hero, hero); //AOE끄기

        }


        public IEnumerator DragCursorMove()
        {
            DungeonUI dungeonUI = (DungeonUI)UIManager.Inst().GetUI(UIEnum.Dungeon);
            DungeonGameUI dungeonGameUI = dungeonUI.GetDungeonGameUI();
            UnitRoot hero = GameManager.Inst().GetUnitRoot(dungeonGameUI.GetSelectedHeroPortraitIndex());

            //이미지 이동
            while (this.gameObject.activeSelf)
            {
                if (move)
                {
                    float moveX = Input.mousePosition.x - holeClone.anchoredPosition.x;
                    float moveY = Input.mousePosition.y - holeClone.anchoredPosition.y;

                    double value = Math.Pow((double)moveX, 2.0f) + Math.Pow((double)moveY, 2.0f);

                    //이미지 활성화 여부 설정
                    left.gameObject.SetActive(false);
                    right.gameObject.SetActive(false);
                    up.gameObject.SetActive(false);
                    down.gameObject.SetActive(false);
                    info.gameObject.SetActive(true);
                    cancle.gameObject.SetActive(false);

                    if (moveY >= Math.Abs(moveX))
                    {
                        up.gameObject.SetActive(true);
                    }
                    else if (-moveY >= Math.Abs(moveX))
                    {
                        down.gameObject.SetActive(true);
                    }
                    else if (moveX >= Math.Abs(moveY))
                    {
                        right.gameObject.SetActive(true);
                    }
                    else if (-moveX >= Math.Abs(moveY))
                    {
                        left.gameObject.SetActive(true);
                    }

                    //커서 안쪽 내부에 있음 (방향 설정 안함)
                    if (value < 5000.0f)
                    {
                        left.gameObject.SetActive(false);
                        right.gameObject.SetActive(false);
                        up.gameObject.SetActive(false);
                        down.gameObject.SetActive(false);
                        info.gameObject.SetActive(false);
                        cancle.gameObject.SetActive(true);
                    }

                    //커서 일정 범위 못벗어남
                    else if (value > 90000.0f)
                    {
                        float ratio = (float)Math.Sqrt(90000.0f / value);

                        moveX *= ratio;
                        moveY *= ratio;
                    }

                    moveX += holeClone.anchoredPosition.x;
                    moveY += holeClone.anchoredPosition.y;

                    move.rectTransform.anchoredPosition = new Vector2(moveX, moveY);


                    //캐릭터 방향 설정 + 모델링 설정

                    List<Weapon> weapons = hero.GetUnit().GetWeaponList();

                    if (up.gameObject.activeSelf == true)
                    {
                        hero.EventStart(MessageType.Signal, null, "LookAt.90", hero, hero, hero);

                        hero.EventStart(MessageType.Signal, null, "Back", hero, hero, hero);

                        for(int i = 0; i < weapons.Count; i++)
                        {
                            weapons[i].transform.localRotation = Quaternion.Euler(0, 0, 90);
                        }
                    }
                    else if (down.gameObject.activeSelf == true)
                    {
                        hero.EventStart(MessageType.Signal, null, "LookAt.270", hero, hero, hero);

                        hero.EventStart(MessageType.Signal, null, "Front", hero, hero, hero);

                        for (int i = 0; i < weapons.Count; i++)
                        {
                            weapons[i].transform.localRotation = Quaternion.Euler(0, 0, 270);
                        }
                    }
                    else if (right.gameObject.activeSelf == true)
                    {
                        hero.EventStart(MessageType.Signal, null, "LookAt.0", hero, hero, hero);

                        hero.EventStart(MessageType.Signal, null, "Flip.Off", hero, hero, hero);

                        hero.EventStart(MessageType.Signal, null, "Front", hero, hero, hero);

                        for (int i = 0; i < weapons.Count; i++)
                        {
                            weapons[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
                        }
                    }
                    else if (left.gameObject.activeSelf == true)
                    {
                        hero.EventStart(MessageType.Signal, null, "LookAt.180", hero, hero, hero);

                        hero.EventStart(MessageType.Signal, null, "Flip.On", hero, hero, hero);

                        hero.EventStart(MessageType.Signal, null, "Front", hero, hero, hero);

                        for (int i = 0; i < weapons.Count; i++)
                        {
                            weapons[i].transform.localRotation = Quaternion.Euler(0, 0, 180);
                        }
                    }

                    if (info.gameObject.activeSelf)
                    {
                        hero.EventStart(MessageType.Signal, null, "Aoe.On", hero, hero, hero);
                    }
                    else
                    {
                        hero.EventStart(MessageType.Signal, null, "Aoe.Off", hero, hero, hero);
                    }
                }

                yield return null;
            }

            yield return null;
        }

        public void CancleButton()
        {
            DungeonUI dungeonUI = (DungeonUI)UIManager.Inst().GetUI(UIEnum.Dungeon);
            HeroInfoUI heroInfoUI = dungeonUI.GetHeroInfoUI();
            DungeonGameUI dungeonGameUI = dungeonUI.GetDungeonGameUI();
            Root hero = GameManager.Inst().GetUnitRoot(dungeonGameUI.GetSelectedHeroPortraitIndex());

            //
            heroInfoUI.CloseUI();

            CloseUI();

            dungeonGameUI.SetSelectedHeroPortraitIndex(-1);

            dungeonGameUI.UpdateDataAll();

            hero.gameObject.SetActive(false);

            hero.EventStart(MessageType.Signal, null, "Aoe.Off", hero, hero, hero); //AOE끄기
        }

    }
}

