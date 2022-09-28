using MangoramaStudio.Scripts.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class KnifeController : CustomBehaviour
{

    public KnifeMovementBehaviour _knifeMovementBehaviour;
    public KnifeHandleCollisionBehaviour _knifeCollisionBehaviour;
    public KnifeTipCollisionBehaviour _knifeTipCollisionBehaviour;
    public SlicingTool _slicingTool;

    private int _defaultPointsPerSlice = 1;
    private int _points;
    private int _sliceComboCount;
    private bool _firstComboGoalReached;
    private bool _secondComboGoalReached;

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        _knifeMovementBehaviour.Initialize(this);
        _knifeCollisionBehaviour.Initialize(this);
        _knifeTipCollisionBehaviour.Initialize(this);
        _slicingTool.Initialize(this);
        _slicingTool.OnObjectSliced += AddPoints;
        _slicingTool.OnObjectSliced += AddAndShowSliceComboPoints;
        _points = 0;
        _sliceComboCount = 0;
    }

    private void OnDestroy()
    {
        
    }

    public void AddPoints()
    {
        _points += _defaultPointsPerSlice;
        GameManager.UIManager.inGamePanel.PopulateView(PlayerData.TotalScore + _points);
    }

    public void AddAndShowSliceComboPoints()
    {
        _sliceComboCount++;
        if ((_sliceComboCount >= 3 && _sliceComboCount <= 6) && !_firstComboGoalReached)
        {
            _defaultPointsPerSlice += 2;
            _firstComboGoalReached = true;
        }
        if (_sliceComboCount >= 6 && !_secondComboGoalReached)
        {
            _defaultPointsPerSlice += 5;
            _secondComboGoalReached = true;
        }
        GameManager.UIManager.inGamePanel.StartShowSliceCounter(_sliceComboCount);
    }

    public void MultiplierCubeReached(MultiplierCubeBehaviour currentMultiplier)
    {
        _points *= currentMultiplier.MultiplierValue;
        PlayerData.TotalScore += _points;
        GameManager.UIManager.inGamePanel.PopulateView(PlayerData.TotalScore);
        GameManager.LevelManager.CurrentLevel.LevelCompleted();
    }

    public void LevelFailed()
    {
        GameManager.LevelManager.CurrentLevel.LevelFailed();
    }

    public void LevelCompletedWithoutMultiplier()
    {
        PlayerData.TotalScore += _points;
        GameManager.LevelManager.CurrentLevel.LevelCompleted();
    }

    public void ResetSliceComboPoints()
    {
        _sliceComboCount = 0;
    }
}
