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
        [SerializeField] private Text _sliceComboText;

        private Coroutine _sliceComboTextRoutine;

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

        public void StartShowSliceCounter(int value)
        {
            if (_sliceComboTextRoutine != null)
            {
                StopCoroutine(_sliceComboTextRoutine);
                _sliceComboTextRoutine = null;
            }
            _sliceComboTextRoutine = StartCoroutine(ShowSliceComboTextCo(value));
        }

        private IEnumerator ShowSliceComboTextCo(int value)
        {
            _sliceComboText.SetActive(true);
            _sliceComboText.text = value.ToString() + "X COMBO!";
            yield return new WaitForSeconds(2f);
            _sliceComboText.SetActive(false);
        }

    }
}