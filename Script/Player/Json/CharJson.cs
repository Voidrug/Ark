using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;
using Data;

namespace Player
{
    [Serializable]
    public class CharData
    {
        [Header("- 캐릭터 번호")]
        [SerializeField] public int charType = -1;

        [Header("- 레벨")]
        [SerializeField] public int level = 0;

        [Header("- 경험치")]
        [SerializeField] public int exp = 0;

        [Header("- 부활대기시간")]
        [SerializeField] public float respawnTime = 0;
    }

    [Serializable]
    public class CharJson
    {
        [SerializeField] private List<CharData> charDatas = new List<CharData>();
        public List<CharData> GetCharDatas() => charDatas;

        public void Init()
        {
            //데이터 칸 할당
/*            for(int i = 0; i < 4; i++)
            {
                charDatas.Add(new CharData());
            }*/
        }

        public void SaveToJson()
        {
            string json = JsonUtility.ToJson(this);
            File.WriteAllText(Application.persistentDataPath + "/CharJson", json);
        }
        public void LoadFromJson() //해당 코드는 1번만 사용하는걸 권장한다 
        {
            // 데이터 로드
            try
            {
                string json = File.ReadAllText(Application.persistentDataPath + "/CharJson");
                JsonUtility.FromJsonOverwrite(json, this);
            }
            catch
            {
                SaveToJson();

                string json = File.ReadAllText(Application.persistentDataPath + "/CharJson");
                JsonUtility.FromJsonOverwrite(json, this);
            }

            //유닛 생성
            PlayerManager playerManager = PlayerManager.Inst();
            List<UnitRoot> unitRoots = playerManager.GetUnitRoots();

            for(int i = 0; i < charDatas.Count; i++)
            {
                Base rootObj = PoolManager.Inst().GetObj(DataManager.Inst().GetCharListRoot()[charDatas[i].charType].gameObject, null, Team.Team00);

                rootObj.gameObject.SetActive(false);
                UnitRoot root = rootObj?.GetComponent<UnitRoot>();
                unitRoots.Add(root);

                rootObj.transform.parent = playerManager.transform;

                Hero hero = root?.GetUnit()?.GetStatus<Hero>();

                if (hero)
                {
                    hero.RemoveData();

                    hero.GetHeroMeta().SetCharType(charDatas[i].charType);
                    hero.GetData().SetLevel(charDatas[i].level);
                    hero.GetData().SetExp(charDatas[i].exp);
                    hero.GetData().SetRespawnTime(charDatas[i].respawnTime);

                    hero.ApplyData();
                }
            }
        }

        public void AddChar(CharData data)
        {
            CharData charData = new CharData();

            charData.charType = data.charType;
            charData.level = data.level;
            charData.exp = data.exp;
            charData.respawnTime = data.respawnTime;

            charDatas.Add(charData);

            //유닛 생성
            PlayerManager playerManager = PlayerManager.Inst();
            List<UnitRoot> unitRoots = playerManager.GetUnitRoots();

            {
                Base rootObj = PoolManager.Inst().GetObj(DataManager.Inst().GetCharListRoot()[charData.charType].gameObject, null, Team.Team00);

                rootObj.gameObject.SetActive(false);
                UnitRoot root = rootObj?.GetComponent<UnitRoot>();
                unitRoots.Add(root);

                rootObj.transform.parent = playerManager.transform;

                Hero hero = root?.GetUnit()?.GetStatus<Hero>();

                if (hero)
                {
                    hero.RemoveData();

                    hero.GetHeroMeta().SetCharType(charData.charType);
                    hero.GetData().SetLevel(charData.level);
                    hero.GetData().SetExp(charData.exp);
                    hero.GetData().SetRespawnTime(charData.respawnTime);

                    hero.ApplyData();
                }
            }

            SaveToJson();
        }
    }
}
