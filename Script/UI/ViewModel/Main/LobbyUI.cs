using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LobbyUI : UIBase
    {
        [Header("- UI View 데이터")]
        [SerializeField] private Text playerName;
        [SerializeField] private Text level;
        [SerializeField] private TMP_Text ap;
        [SerializeField] private Text apMax;
        [SerializeField] private TMP_Text money;
        [SerializeField] private TMP_Text diamond;
        [SerializeField] private TMP_Text originium;
        [SerializeField] private Image levelProgress;
        [SerializeField] private TMP_Text realTime;
        void Awake()
        {
            //1회성 초기화
            UIManager.Inst().AddUI(UIEnum.Lobby, this);
        }

        void Start()
        {
            UpdateDataAll();
        }

        public override void OpenUI()
        {
            Init();

            UpdateDataAll();
        }
        public override void CloseUI()
        {
            StartCoroutine(CloseCanvas(0.5f));
        }
        public override void Init()
        {
            //캔버스 열기
            gameObject.SetActive(true);

            StartCoroutine(OpenCanvas(0.5f));

            //시간 활성화
            StartCoroutine(UpdateTime());
        }

        public override void UpdateDataAll()
        {
            UpdateData();
        }

        public override void UpdateData()
        {
            PlayerJson meta = PlayerManager.Inst().GetPlayerJson();

            if (meta != null)
            {
                if (playerName)
                    playerName.text = meta.GetPlayerName();
                if (level)
                    level.text = meta.GetLevel().ToString();
                if (ap)
                    ap.text = meta.GetAP().ToString();
                if (apMax)
                    apMax.text = "이성/" + meta.GetApMax().ToString();
                if (money)
                    money.text = meta.GetMoney().ToString();
                if (diamond)
                    diamond.text = meta.GetDiamond().ToString();
                if (originium)
                    originium.text = meta.GetOriginium().ToString();
                if (levelProgress)
                    levelProgress.fillAmount = (float)meta.GetExp() / (float)meta.GetExpMax();
            }
        }

        public IEnumerator UpdateTime()
        {
            while (gameObject.activeSelf)
            {
                if (realTime)
                    realTime.text = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                yield return new WaitForSeconds(1.0f);
            }

            yield return null;
        }

    }
}
