using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Tools;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    [Serializable]
    public class PlayerAliance
    {
        [SerializeField] protected Aliance[] aliances = new Aliance[3];

        public Aliance GetAliance(int target) => aliances[target];
    }



    [Serializable]
    public class PlayerManager : MonoSingle<PlayerManager>
    {
        [SerializeField] private PlayerJson playerJson = new PlayerJson();
        [SerializeField] private CharJson charJson = new CharJson();
        [SerializeField] private SquadJson squadJson = new SquadJson();
        [SerializeField] private SelectJson selectJson = new SelectJson();

        [SerializeField, System.NonSerialized] private List<UnitRoot> rootObjects = new List<UnitRoot>();

        [SerializeField] private PlayerAliance[] aliances = new PlayerAliance[3];

        private int squadTeamIndex = 0;

        public PlayerManager()
        {
            instance = this;
        }

        private void Start()
        {
            //유니티는 생성자로 칸을 초기화하면 하이어라키에 제대로 적용이 안되는 경우가 많음
            //데이터 로드
            PlayerLoad();

            charJson.Init();
            CharLoad();

            squadJson.Init();
            SquadLoad();

            selectJson.Init();
            SelectLoad();
        }


        public void SetSquadTeamIndex(int value) => squadTeamIndex = value;
        public PlayerJson GetPlayerJson() => playerJson;
        public CharJson GetCharJson() => charJson;
        public SquadJson GetSquadJson() => squadJson;
        public SelectJson GetSelectJson() => selectJson;
        public List<UnitRoot> GetUnitRoots() => rootObjects;
        public int GetSquadTeamIndex() => squadTeamIndex;
        public Aliance GetAliance(int oneself, int target) => aliances[oneself].GetAliance(target);
        public void PlayerSave() => playerJson.SaveToJson();
        public void CharSave() => charJson.SaveToJson();
        public void SquadSave() => squadJson.SaveToJson();
        public void SelectSave() => selectJson.SaveToJson();
        public void PlayerLoad() => playerJson.LoadFromJson();
        public void CharLoad() => charJson.LoadFromJson();
        public void SquadLoad() => squadJson.LoadFromJson();
        public void SelectLoad() => selectJson.LoadFromJson();

        public void AddChar(CharData charData)
        {
            charJson.AddChar(charData);
            selectJson.AddChar(charData);

            CharSave();
            SelectSave();
        }

    }
}
