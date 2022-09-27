using MangoramaStudio.Scripts.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : CustomBehaviour
{
    [SerializeField] private List<LevelBehaviour> _levelBehaviours;

    private LevelBehaviour _currentLevel;

    #region Initialize

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);

        GameManager.EventManager.OnStartGame += StartGame;
    }

    private void OnDestroy()
    {
        GameManager.EventManager.OnStartGame -= StartGame;
    }

    #endregion

    private void StartGame()
    {
        ClearLevel();

        InputController.IsInputDeactivated = false;

        var currentLevelId = PlayerData.CurrentLevelId - 1;

        currentLevelId = currentLevelId != 0 ? currentLevelId % _levelBehaviours.Count : currentLevelId;

        if (_levelBehaviours.Count > 0)
        {
            var levelBehaviour = Instantiate(_levelBehaviours[currentLevelId]);

            _currentLevel = levelBehaviour;

            levelBehaviour.Initialize(GameManager);
        }
        else
        {
            Debug.LogError("No Levels found!");
        }
    }

    private void ClearLevel()
    {
        if (_currentLevel != null)
        {
            Destroy(_currentLevel.gameObject);
        }
    }

    public void ContinueToNextLevel() // For button
    {
        PlayerData.CurrentLevelId += 1;
        StartGame();
    }

    public void RetryCurrentLevel() // For button
    {
        StartGame();
    }
}

