using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Level
{
    [Serializable]
    public class DungeonData
    {
        [Header("- 초기 코스트")]
        [SerializeField] private float cost;

        [Header("- 코스트 재생")]
        [SerializeField] private float costRegen;

        [Header("- 배치인원제한")]
        [SerializeField] private int battleCount;

        [Header("- 목숨")]
        [SerializeField] private int lifeCount;

        public float GetCost() => cost;
        public float GetCostRegen() => costRegen;
        public int GetBattleCount() => battleCount;
        public int GetLifeCount() => lifeCount;
        public void SetCost(float value) => cost = value;
        public void SetCostRegen(float value) => costRegen = value;
        public void SetBattleCount(int value) => battleCount = value;
        public void SetLifeCount(int value) => lifeCount = value;
    }
}
