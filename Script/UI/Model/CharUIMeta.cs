using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UI
{
    [Serializable]
    [CreateAssetMenu(fileName = "CharUIData", menuName = "UI/CharUIMeta")]
    public class CharUIMeta : ScriptableObject
    {
        [Header("- UI Back 이미지")]
        [SerializeField] private List<Sprite> backImage = new List<Sprite>();

        [Header("- UI Middle 이미지")]
        [SerializeField] private List<Sprite> middleImage = new List<Sprite>();

        [Header("- UI Up 이미지")]
        [SerializeField] private List<Sprite> upImage = new List<Sprite>();

        [Header("- UI Tier 이미지")]
        [SerializeField] private List<Sprite> tierImage = new List<Sprite>();

        [Header("- UI Front 이미지")]
        [SerializeField] private List<Sprite> frontImage = new List<Sprite>();

        [Header("- UI Elite 이미지")]
        [SerializeField] private List<Sprite> eliteImage = new List<Sprite>();

        [Header("- UI Attribute 이미지")]
        [SerializeField] private List<Sprite> attributeImage = new List<Sprite>();

        [Header("- UI Attribute Large 이미지")]
        [SerializeField] private List<Sprite> attributeLargeImage = new List<Sprite>();

        public Sprite GetBackImage(int index) => backImage[index];
        public Sprite GetMiddleImage(int index) => middleImage[index];
        public Sprite GetUpImage(int index) => upImage[index];
        public Sprite GetTierImage(int index) => tierImage[index];
        public Sprite GetFrontImage(int index) => frontImage[index];
        public Sprite GetEliteImage(int index) => eliteImage[index];
        public Sprite GetAttributeImage(int index) => attributeImage[index];
        public Sprite GetAttributeLargeImage(int index) => attributeLargeImage[index];
    }
}
