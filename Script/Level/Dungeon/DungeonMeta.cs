using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEditor.AddressableAssets.Build.Layout.BuildLayout;

namespace Level
{
    [Serializable]
    public struct MonsterSpawn
    {
        [SerializeField] private string spawnUnit;
        [SerializeField] private float spawnTime;
        [SerializeField] private PathOrderMeta pathOrderMeta;
    }

    [Serializable]
    [CreateAssetMenu(fileName = "DungeonData", menuName = "Level/DungeonMeta")]
    public class DungeonMeta : ScriptableObject
    {
        [Header("- 정적 데이터")]

        [Header("- 타일 메테리얼")]
        [SerializeField] private Material tileMtrl;

        [Header("- 타일 위치 메테리얼")]
        [SerializeField] private Material tilePosMtrl;

        [Header("- 몬스터 정보")]
        [SerializeField] private List<MonsterSpawn> monsterSpawns;

        [Header("- 동적 데이터")]
        [SerializeField] private DungeonData dungeonData;

        public List<MonsterSpawn> GetMonsterSpawns() => monsterSpawns;
        public Material GetTileMaterial() => tileMtrl;
        public Material GetTilePosMaterial() => tilePosMtrl;
        public DungeonData GetDungeonData() => dungeonData;
    }
}
