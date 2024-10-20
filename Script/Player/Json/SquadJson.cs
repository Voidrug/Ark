using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;
using Data;
using System.Drawing;

namespace Player
{
    [Serializable]
    public class SquadData
    {
       [SerializeField] public int[] charNums = new int[12];
    }


    [Serializable]
    public class SquadJson
    {
        [SerializeField] private List<SquadData> squadCharNums = new List<SquadData>();


        public SquadData GetSquadCharNums() => squadCharNums[PlayerManager.Inst().GetSquadTeamIndex()];
        public int GetSquadCharNum(int index) => index == -1 ? -1 : squadCharNums[PlayerManager.Inst().GetSquadTeamIndex()].charNums[index];
        public void SetSquadCharNum(int value, int index)
        {
            if(index != -1)
            {
                squadCharNums[PlayerManager.Inst().GetSquadTeamIndex()].charNums[index] = value;
            }
        }

        public void Init()
        {
            //데이터 칸 할당
            for (int i = 0; i < 4; i++)
            {
                squadCharNums.Add(new SquadData());

                for(int j = 0; j < 12; j++)
                {
                    squadCharNums[i].charNums[j] = -1;
                }
            }
        }
        public void SaveToJson()
        {
            string json = JsonUtility.ToJson(this);
            File.WriteAllText(Application.persistentDataPath + "/SquadJson", json);
        }
        public void LoadFromJson()
        {
            // 데이터 로드
            try
            {
                string json = File.ReadAllText(Application.persistentDataPath + "/SquadJson");
                JsonUtility.FromJsonOverwrite(json, this);
            }
            catch
            {
                SaveToJson();

                string json = File.ReadAllText(Application.persistentDataPath + "/SquadJson");
                JsonUtility.FromJsonOverwrite(json, this);
            }
        }
    }
}
