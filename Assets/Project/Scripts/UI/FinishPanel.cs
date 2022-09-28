using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishPanel : UIPanel
{

    public GameObject WinPanel;
    public GameObject LosePanel;

    public Button ButtonNext;
    public Button ButtonRetry;


    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);

        GameManager.EventManager.OnLevelFinished += SetWinLosePanel;
        GameManager.EventManager.OnLevelStarted += RefreshPanels;
    }


    private void OnDestroy()
    {
        GameManager.EventManager.OnLevelFinished -= SetWinLosePanel;
        GameManager.EventManager.OnLevelStarted -= RefreshPanels;
    }

    /****************************************************************************************/
    private void RefreshPanels()
    {
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    private void SetWinLosePanel(bool isSuccess)
    {
        ShowPanel();

        StartCoroutine(SetWinLosePanelCo(isSuccess));
    }

    private IEnumerator SetWinLosePanelCo(bool isSuccess)
    {
        var delayTime = isSuccess == true ? FindObjectOfType<LevelBehaviour>().WinPanelDelayTime : FindObjectOfType<LevelBehaviour>().LosePanelDelayTime;

        yield return new WaitForSeconds(delayTime);

        WinPanel.SetActive(isSuccess);
        LosePanel.SetActive(!isSuccess);
    }


    public void ClickNext()
    {
        Debug.Log("continue");
        GameManager.LevelManager.ContinueToNextLevel();
    }

    public void ClickRetry()
    {
        GameManager.LevelManager.RetryCurrentLevel();
    }
}
