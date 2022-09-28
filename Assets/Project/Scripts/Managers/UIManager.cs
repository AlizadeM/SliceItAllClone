using MangoramaStudio.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class UIManager : CustomBehaviour
{
    public MainMenuPanel mainMenuPanel;
    public InGamePanel inGamePanel;
    public FinishPanel finishPanel;

    private List<UIPanel> UIPanels;

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        UIPanels = new List<UIPanel> { mainMenuPanel, finishPanel, inGamePanel };

        UIPanels.ForEach(x =>
        {
            x.Initialize(this);
            x.gameObject.SetActive(false);
        });
        inGamePanel.ShowPanel();
    }
}