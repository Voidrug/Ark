using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;
using Data;
using UnityEditor.Build.Pipeline;

namespace Player
{
    [Serializable]
    public class SelectData
    {
        [SerializeField] public List<int> abilityNums = new List<int>();
    }

    [Serializable]
    public class SelectJson
    {
        [SerializeField] private List<SelectData> selectAbilityNums = new List<SelectData>();
        [SerializeField] private SelectData selectTempNums = new SelectData();

        public int GetSelectAbilityNum(int index) => index == -1 ? -1 : selectAbilityNums[PlayerManager.Inst().GetSquadTeamIndex()].abilityNums[index];
        public int GetSelectTempNum(int index) => index == -1 ? -1 : selectTempNums.abilityNums[index];
        public void SetSelectAbilityNum(int value, int index)
        {
            if(index != -1)
            {
                selectAbilityNums[PlayerManager.Inst().GetSquadTeamIndex()].abilityNums[index] = value;
            }
        }
        public void SetSelectTempNum(int value, int index)
        {
            if(index != -1)
            {
                selectTempNums.abilityNums[index] = value;
            }
        }

        public void Init()
        {
            //데이터 칸 할당
            List<CharData> charDatas = PlayerManager.Inst().GetCharJson().GetCharDatas();

            for (int i = 0; i < 4; i++)
            {
                selectAbilityNums.Add(new SelectData());

                for (int j = 0; j < charDatas.Count; j++)
                {
                    selectAbilityNums[i].abilityNums.Add(-1);
                }
            }

            for (int i = 0; i < charDatas.Count; i++)
            {
                selectTempNums.abilityNums.Add(-1);
            }
        }

        public void SaveToJson()
        {
            string json = JsonUtility.ToJson(this);
            File.WriteAllText(Application.persistentDataPath + "/SelectJson", json);
        }
        public void LoadFromJson()
        {
            // 데이터 로드
            try
            {
                string json = File.ReadAllText(Application.persistentDataPath + "/SelectJson");
                JsonUtility.FromJsonOverwrite(json, this);
            }
            catch
            {
                SaveToJson();

                string json = File.ReadAllText(Application.persistentDataPath + "/SelectJson");
                JsonUtility.FromJsonOverwrite(json, this);
            }
        }

        public void SetTempNums(int index)
        {
            for(int i = 0; i < selectAbilityNums.Count; i++)
            {
                selectTempNums.abilityNums[i] = selectAbilityNums[PlayerManager.Inst().GetSquadTeamIndex()].abilityNums[i];
            }
        }

        public void AddChar(CharData charData)
        {
            // 데이터 칸 설정
            List<UnitRoot> unitRoots = PlayerManager.Inst().GetUnitRoots();

            for (int i = 0; i < 4; i++)
            {
                while (selectAbilityNums[i].abilityNums.Count < unitRoots.Count)
                {
                    selectAbilityNums[i].abilityNums.Add(-1);
                }
            }

            while (selectTempNums.abilityNums.Count < unitRoots.Count)
            {
                selectTempNums.abilityNums.Add(-1);
            }
        }
    }
}
