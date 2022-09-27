﻿public class MainMenuPanel : UIPanel
{
    public CustomButton ButtonPlay;
    /****************************************************************************************/

    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);
        ButtonPlay.Initialize(uiManager, StartGame);
    }

    private void OnDestroy()
    {
    }

    /****************************************************************************************/
    private void StartGame()
    {
        UIManager.inGamePanel.ShowPanel();
        UIManager.mainMenuPanel.HidePanel();
    }
}