using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.UIElements;
using UnityEngine;

namespace Level
{
    [Serializable]
    public class Dungeon : MonoBehaviour
    {
        [Header("- 블록 데이터")]
        [SerializeField] private List<BoxCollider> Floors = new List<BoxCollider>();
        [SerializeField] private List<BoxCollider> Cliffs = new List<BoxCollider>();

        [Header("- 메타 데이터")]
        [SerializeField] private DungeonMeta dungeonMeta;

        [Header("- 인게임 데이터")]
        [SerializeField, ReadOnly] private DungeonData dungeonData;

        [SerializeField, ReadOnly] private int enemyCount;


        public DungeonMeta GetDungeonMeta() => dungeonMeta;
        public DungeonData GetDungeonData() => dungeonData;
        public int GetEnemyCount() => enemyCount;
        public void SetDungeonMeta(DungeonMeta meta) => dungeonMeta = meta;
        public void SetDungeonData(DungeonData data) => dungeonData = data;
        public void SetEnemyCount(int value) => enemyCount = value;

        private void Awake()
        {
            Init();
        }


        public void Init()
        {
            foreach (Transform child in transform)
            {
                if(child.tag == "Floor")
                {
                    Floors.Add(child.GetComponent<BoxCollider>());
                }
                else if(child.tag == "Cliff")
                {
                    Cliffs.Add(child.GetComponent<BoxCollider>());
                }
            }
        }

        public void UpdateDataAll()
        {
            UpdateData();
        }

        public void UpdateData()
        {
            //데이터 주기적 초기화

            enemyCount = dungeonMeta.GetMonsterSpawns().Count;

            dungeonData.SetBattleCount(dungeonMeta.GetDungeonData().GetBattleCount());
            dungeonData.SetCost(dungeonMeta.GetDungeonData().GetCost());
            dungeonData.SetCostRegen(dungeonMeta.GetDungeonData().GetCostRegen());
            dungeonData.SetLifeCount(dungeonMeta.GetDungeonData().GetLifeCount());
        }

        public void SetTileColor(HeroMeta heroMeta, bool active)
        {
            if(active) //활성화시에 
            {
                bool check;

                UnitPos unitPos = heroMeta.GetUnitPos();

                check = (unitPos & UnitPos.Floor) == UnitPos.Floor ? true : false;

                for (int i = 0; i < Floors.Count; i++)
                {
                    if (check)
                    {
                        Floors[i].GetComponent<MeshRenderer>().material = dungeonMeta.GetTilePosMaterial();
                    }
                    else
                    {
                        Floors[i].GetComponent<MeshRenderer>().material = dungeonMeta.GetTileMaterial();
                    }

                    Floors[i].enabled = check;
                }

                check = (unitPos & UnitPos.Cliff) == UnitPos.Cliff ? true : false;

                for (int i = 0; i < Cliffs.Count; i++)
                {
                    if (check)
                    {
                        Cliffs[i].GetComponent<MeshRenderer>().material = dungeonMeta.GetTilePosMaterial();
                    }
                    else
                    {
                        Cliffs[i].GetComponent<MeshRenderer>().material = dungeonMeta.GetTileMaterial();
                    }

                    Cliffs[i].enabled = check;
                }
            }
            else //비활성화
            {
                for (int i = 0; i < Floors.Count; i++)
                {
                    Floors[i].GetComponent<MeshRenderer>().material = dungeonMeta.GetTileMaterial();
                }

                for (int i = 0; i < Cliffs.Count; i++)
                {
                    Cliffs[i].GetComponent<MeshRenderer>().material = dungeonMeta.GetTileMaterial();
                }
            }

        }
    }
}
