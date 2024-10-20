using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;
using UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEditor.SearchService;
using Player;
using Data;

namespace Level
{
    public class LevelManager : MonoSingle<LevelManager>
    {
        [SerializeField] private Camera mainCam;
        [SerializeField] private Camera unitCam;

        private MissionMeta missionMeta;
        private Dungeon dungeonPrefab;

        private Dungeon dungeon;

        public Camera GetMainCamera() => mainCam;
        public Camera GetUnitCamera() => unitCam;
        public MissionMeta GetMissionMeta() => missionMeta;
        public Dungeon GetDungeonPrefab() => dungeonPrefab;
        public Dungeon GetDungeon() => dungeon;
        public void SetMainCamera(Camera camera) => mainCam = camera;
        public void SetUnitCamera(Camera camera) => unitCam = camera;
        public void SetMissionMeta(MissionMeta meta) => missionMeta = meta;
        public void SetDungeonPrefab(Dungeon prefab) => dungeonPrefab = prefab;
        public void SetDungeon(Dungeon obj) => dungeon = obj;

        public LevelManager()
        {
            instance = this;
        }

        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneCoroutine(sceneName));
        }

        private IEnumerator LoadSceneCoroutine(string sceneName)
        {
            //로드인 ui 호출

            yield return new WaitForSeconds(0.5f);

            LoadInUI loadInUI = (LoadInUI)UIManager.Inst().GetUI(UIEnum.LoadIn);

            loadInUI.OpenUI();

            yield return new WaitForSeconds(1.0f);

            //새로운 씬 비동기 로드
            AsyncOperation asyncLoadin = SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);

            while (!asyncLoadin.isDone)
            {
                LoadOutUI loadOutUI = (LoadOutUI)UIManager.Inst().GetUI(UIEnum.LoadOut);

                while(loadOutUI == null)
                {
                    loadOutUI = (LoadOutUI)UIManager.Inst().GetUI(UIEnum.LoadOut);

                    Debug.Log("Wait");

                    yield return null;
                }

                loadOutUI.OpenUI();

                yield return new WaitForSeconds(0.5f);

                AsyncOperation asyncLoadOut = SceneManager.UnloadSceneAsync("1. Lobby");

                while (!asyncLoadOut.isDone)
                {
                    Debug.Log("Wait");

                    yield return null;
                }

                yield return new WaitForSeconds(0.5f);

                loadOutUI.CloseUI();

                //던전 데이터 생성

                if(dungeonPrefab != null)
                {
                    dungeon = Instantiate(dungeonPrefab.gameObject).GetComponent<Dungeon>();

                    dungeon.UpdateDataAll();

                    SceneManager.MoveGameObjectToScene(dungeon.gameObject, SceneManager.GetSceneByName(sceneName));
                }

                DungeonUI dungeonUI = null;

                while(!dungeonUI)
                {
                    dungeonUI = (DungeonUI)UIManager.Inst().GetUI(UIEnum.Dungeon);

                    Debug.Log("Wait1");

                    yield return null;
                }

                //유닛 생성
                PlayerManager playerManager = PlayerManager.Inst();
                CharJson charJson = playerManager.GetCharJson();
                SquadJson squadJson = playerManager.GetSquadJson();
                int sqaudIndex = playerManager.GetSquadTeamIndex();
                List<CharData> charDatas = charJson.GetCharDatas();
                List<UnitRoot> unitRoots = GameManager.Inst().GetUnitRoots();
                SquadData squadCharNum = squadJson.GetSquadCharNums();

                for (int i = 0; i < squadCharNum.charNums.Length; i++)
                {
                    if (squadCharNum.charNums[i] == -1)
                        continue;

                    Base rootObj = PoolManager.Inst().GetObj(DataManager.Inst().GetCharListRoot()[charDatas[squadCharNum.charNums[i]].charType].gameObject, null, Team.Team00);
                    rootObj.gameObject.SetActive(false);
                    UnitRoot root = rootObj?.GetComponent<UnitRoot>();
                    unitRoots.Add(root);

                    rootObj.transform.parent = GameManager.Inst().transform;

                    Hero hero = root?.GetUnit()?.GetStatus<Hero>();

                    if (hero)
                    {
                        hero.RemoveData();

                        hero.GetHeroMeta().SetCharType(charDatas[squadCharNum.charNums[i]].charType);
                        hero.GetData().SetLevel(charDatas[squadCharNum.charNums[i]].level);
                        hero.GetData().SetExp(charDatas[squadCharNum.charNums[i]].exp);
                        hero.GetData().SetRespawnTime(charDatas[squadCharNum.charNums[i]].respawnTime);

                        hero.ApplyData();
                    }
                }

                dungeonUI.OpenUI();

                yield return null;
            }
        }

        public void SetTileColor(HeroMeta heroMeta, bool active)
        {
            if(dungeon)
            {
                dungeon.SetTileColor(heroMeta, active);
            }
        }
    }

}
