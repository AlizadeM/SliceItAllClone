using Cinemachine;
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
    [SerializeField] private KnifeController _knifeController;
    [SerializeField] private Transform _knifeStartTransform;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _knifeFollowCam;
    [SerializeField] private Vector3 _cameraOffset;

    private bool _isLevelEnded;

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        GameManager.EventManager.StartLevel();
        var knife = Instantiate(_knifeController.gameObject,_knifeStartTransform.position,Quaternion.identity,transform);
        knife.GetComponent<KnifeController>().Initialize(gameManager);
        _knifeFollowCam.Follow = knife.transform;
        var transposer = _knifeFollowCam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = new Vector3(-15,40,-53);
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

    public void LevelCompleted()
    {
        if (_isLevelEnded) return;

        GameManager.EventManager.LevelCompleted();
        InputController.IsInputDeactivated = true;
        _isLevelEnded = true;
    }

    public void LevelFailed()
    {
        if (_isLevelEnded) return;

        GameManager.EventManager.LevelFailed();
        InputController.IsInputDeactivated = true;
        _isLevelEnded = true;
    }
}
