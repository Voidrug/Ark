using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [Serializable]
    [CreateAssetMenu(fileName = "HeroData", menuName = "Base/Status/HeroMeta")]
    public class HeroMeta : StatusMeta
    {
        [Header("- 정적 데이터")]

        [Header("- 영웅 이름")]
        [SerializeField] private string charName;

        [Header("- 캐릭터 등급")]
        [SerializeField] private CharTier charTier = CharTier.One;

        [Header("- 레벨당 스텟")]
        [SerializeField] private List<HeroExtraStatData> heroExtraStats;

        [Header("- UI 유닛 이미지")]
        [SerializeField] private Sprite unitImage;

        [Header("- 유닛 이미지 피벗")]
        [SerializeField] private Vector2 unitPivot = new Vector2(0.5f, 0.5f);

        [Header("- 유닛 이미지 배율")]
        [SerializeField] private Vector2 unitScale = new Vector2(1.0f, 1.0f);

        [Header("- UI 초상화 이미지")]
        [SerializeField] private Sprite portraitImage;

        [Header("- UI 블록 이미지")]
        [SerializeField] private Sprite blockImage;

        [Header("- UI 직업군 작음 이미지")]
        [SerializeField] private Sprite attributeSmallImage;

        [Header("- 캐릭터 번호")]
        [SerializeField] private int charType = -1;

        [Header("- 배치 여부")]
        [SerializeField] private UnitPos unitPos;

        [Header("- 동적 데이터")]
        [SerializeField] private HeroData heroData;


        public string GetCharName() => charName;
        public CharTier GetCharTier() => charTier;
        public List<HeroExtraStatData> GetHeroExtraStatDatas() => heroExtraStats;
        public Sprite GetUnitImage() => unitImage;
        public Vector2 GetUnitPivot() => unitPivot;
        public Vector2 GetInitScale() => unitScale;
        public Sprite GetPortraitImage() => portraitImage;
        public Sprite GetBlockImage() => blockImage;
        public Sprite GetAttributeSmallImage() => attributeSmallImage;
        public int GetCharType() => charType;
        public UnitPos GetUnitPos() => unitPos;
        public HeroData GetHeroData() => heroData;
        public void SetCharType(int value) => charType = value;
        


    }
}
