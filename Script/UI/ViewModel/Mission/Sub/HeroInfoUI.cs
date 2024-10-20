using Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEditor.AddressableAssets.Build.Layout.BuildLayout;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Data;

namespace UI
{
    public class HeroInfoUI : UIBase
    {
        [Header("- UI View 데이터")]
        [SerializeField] private Image info;

        void Awake()
        {
            gameObject.SetActive(false);
        }

        public override void OpenUI()
        {
            Init();

            UpdateDataAll();
        }
        public override void CloseUI()
        {
            StartCoroutine(CloseCanvas(0.25f));

            StartCoroutine(CameraSilderDown(0.1f));
        }
        public override void Init()
        {
            gameObject.SetActive(true);

            StartCoroutine(OpenCanvas(0.25f));

            StartCoroutine(CameraSilderUp(0.1f));
        }
        public override void UpdateDataAll()
        {
            UpdateData();
        }

        public override void UpdateData()
        {
            if (gameObject.activeSelf == false)
                return;

            if (info)
            {
                DungeonUI dungeonUI = (DungeonUI)UIManager.Inst().GetUI(UIEnum.Dungeon);
                DungeonGameUI dungeonGameUI = dungeonUI.GetDungeonGameUI();
                UnitRoot root = GameManager.Inst().GetUnitRoot(dungeonGameUI.GetSelectedHeroPortraitIndex());
                Unit unit = root.GetUnit();
                Hero hero = unit.GetStatus<Hero>();
                if (root == null)
                    return;

                Vector2 unitPivot = hero.GetHeroMeta().GetUnitPivot();
                Vector2 unitScale = hero.GetHeroMeta().GetInitScale();



                if (hero)
                {
                    info.sprite = hero.GetHeroMeta().GetUnitImage();
                    info.rectTransform.pivot = unitPivot;
                    info.rectTransform.localScale = unitScale;
                }

                info.rectTransform.anchoredPosition = new Vector3(0.0f, 0.0f, 0.0f);
                info.color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
            }
        }

        public IEnumerator CameraSilderUp(float fullTime)
        {
            //카메라 회전
            LevelManager levelManager = LevelManager.Inst();
            Camera camera = levelManager.GetMainCamera();

            float time = 0.0f;

            while (time < fullTime)
            {
                time += Time.unscaledDeltaTime;

                camera.transform.localPosition = new Vector3(0.0f, -2.25f, Mathf.Lerp(-5.5f, -6.0f, time / fullTime));
                camera.transform.localRotation = Quaternion.Euler(-20.0f, Mathf.Lerp(0.0f, -5.0f, time / fullTime), Mathf.Lerp(0.0f, -2.0f, time / fullTime));

                yield return null;
            }
            yield return null;
        }

        public IEnumerator CameraSilderDown(float fullTime)
        {
            //카메라 회전
            LevelManager levelManager = LevelManager.Inst();
            Camera camera = levelManager.GetMainCamera();

            float time = 0.0f;

            while (time < fullTime)
            {
                time += Time.unscaledDeltaTime;

                camera.transform.localPosition = new Vector3(0.0f, -2.25f, Mathf.Lerp(-6.0f, -5.5f, time / fullTime));
                camera.transform.localRotation = Quaternion.Euler(-20.0f, Mathf.Lerp(1-5.0f, 0.0f, time / fullTime), Mathf.Lerp(-2.0f, 0.0f, time / fullTime));

                yield return null;
            }
            yield return null;
        }

        public IEnumerator InfoSlider(float fullTime)
        {
            float time = 0.0f;

            while (time < fullTime)
            {
                time += Time.unscaledDeltaTime;

                if (info)
                {
                    info.rectTransform.anchoredPosition = new Vector3(Mathf.Lerp(0.0f, -300.0f, time / fullTime), 0.0f, 0.0f);
                    info.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, Mathf.Lerp(255.0f, 150.0f, time / fullTime) / 255f);
                }                

                yield return null;
            }

            yield return null;
        }
    }
}
