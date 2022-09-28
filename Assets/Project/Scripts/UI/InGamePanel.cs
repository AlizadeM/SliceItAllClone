using MangoramaStudio.Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MangoramaStudio.Scripts.UI
{
    public class InGamePanel : UIPanel
    {

        [SerializeField] private Text _totalMoneyText;

        public override void Initialize(UIManager uiManager)
        {
            base.Initialize(uiManager);
            PopulateView(PlayerData.TotalScore);
        }

        private void OnDestroy()
        {
            
        }

        public void PopulateView(int value)
        {
            _totalMoneyText.text = value.ToString();
        }

    }
}