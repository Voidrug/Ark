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
    public class PlayerJson
    {
        [SerializeField] private string playerName;

        [SerializeField] private int level;

        [SerializeField] private int levelMax;

        [SerializeField] private int exp;

        [SerializeField] private int expMax;

        [SerializeField] private int ap;

        [SerializeField] private int apMax;

        [SerializeField] private int money;

        [SerializeField] private int diamond;

        [SerializeField] private int originium;

        public string GetPlayerName() => playerName;
        public int GetAP() => ap;
        public int GetLevel() => level;
        public int GetLevelMax() => levelMax;
        public int GetExp() => exp;
        public int GetExpMax() => expMax;
        public int GetAp() => ap;
        public int GetApMax() => apMax;
        public int GetMoney() => money;
        public int GetDiamond() => diamond;
        public int GetOriginium() => originium;

        public void SaveToJson()
        {
            string json = JsonUtility.ToJson(this);
            File.WriteAllText(Application.persistentDataPath + "/PlayerJson", json);
        }

        public void LoadFromJson()
        {
            // 데이터 로드
            try
            {
                string json = File.ReadAllText(Application.persistentDataPath + "/PlayerJson");
                JsonUtility.FromJsonOverwrite(json, this);
            }
            catch
            {
                SaveToJson();

                string json = File.ReadAllText(Application.persistentDataPath + "/PlayerJson");
                JsonUtility.FromJsonOverwrite(json, this);
            }
        }
    }
}
