using MangoramaStudio.Scripts.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class KnifeController : CustomBehaviour
{

    public int PointsPerObject => _pointsPerObject;

    [SerializeField] private int _pointsPerObject;
    private int _points;

    public KnifeMovementBehaviour _knifeMovementBehaviour;
    public KnifeHandleCollisionBehaviour _knifeCollisionBehaviour;
    public KnifeTipCollisionBehaviour _knifeTipCollisionBehaviour;
    public SlicingTool _slicingTool;

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        _knifeMovementBehaviour.Initialize(this);
        _knifeCollisionBehaviour.Initialize(this);
        _knifeTipCollisionBehaviour.Initialize(this);
        _slicingTool.Initialize(this);
        _slicingTool.OnObjectSliced += AddPoints;
        _points = 0;
    }

    public void AddPoints()
    {
        _points += _pointsPerObject;
        GameManager.UIManager.inGamePanel.PopulateView(PlayerData.TotalScore + _points);
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
}
