using Data;
using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class SquadTeamUI : UIBase
    {
        [Header("- UI View 데이터")]
        [SerializeField] private Button hover;

        [Header("- UI Field 데이터")]
        [SerializeField] private int teamIndex = 0;

        public void SetTeamIndex(int index) => teamIndex = index;
        public int GetTeamIndex() => teamIndex;

        public override void OpenUI()
        {
            Init();

            UpdateDataAll();
        }
        public override void CloseUI()
        {
            gameObject.SetActive(false);
        }
        public override void Init()
        {
            gameObject.SetActive(true);
        }
        public override void UpdateDataAll()
        {
            UpdateData();
        }
        public override void UpdateData()
        {
            int selectedTeamIndex = PlayerManager.Inst().GetSquadTeamIndex();

            if (teamIndex == selectedTeamIndex)
            {
                hover.gameObject.SetActive(true);
            }
            else
            {
                hover.gameObject.SetActive(false);
            }
        }

        public void ClickTeamButton()
        {
            SquadUI squadUI = (SquadUI)UIManager.Inst().GetUI(UIEnum.Squad);

            PlayerManager.Inst().SetSquadTeamIndex(teamIndex);

            if (squadUI)
            {
                squadUI.SetSelectedSquadTeamIndex(teamIndex);
                squadUI.UpdateDataAll();
            }
        }
    }
}
