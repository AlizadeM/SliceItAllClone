using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBehaviour : CustomBehaviour
{
    public float WinPanelDelayTime => _winPanelDelayTime;
    public float LosePanelDelayTime => _losePanelDelayTime;

    [SerializeField] private float _winPanelDelayTime;
    [SerializeField] private float _losePanelDelayTime;

    private bool _isLevelEnded;

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);

        GameManager.EventManager.StartLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            LevelCompleted();
        }

        if (Input.GetKeyDown("f"))
        {
            LevelFailed();
        }
    }

    private void OnDestroy()
    {
        
    }

    private void LevelCompleted()
    {
        if (_isLevelEnded) return;

        GameManager.EventManager.LevelCompleted();
        InputController.IsInputDeactivated = true;
        _isLevelEnded = true;
    }

    private void LevelFailed()
    {
        if (_isLevelEnded) return;

        GameManager.EventManager.LevelFailed();
        InputController.IsInputDeactivated = true;
        _isLevelEnded = true;
    }
}
