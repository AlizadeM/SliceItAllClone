using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : UIPanel
{
    [SerializeField] private Button _startButton;
    /****************************************************************************************/

    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);
    }

    private void OnDestroy()
    {

    }

    /****************************************************************************************/

    public void ShowMainMenu()
    {
        Time.timeScale = 0;
        ShowPanel();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        UIManager.inGamePanel.ShowPanel();
        UIManager.mainMenuPanel.HidePanel();
    }
}