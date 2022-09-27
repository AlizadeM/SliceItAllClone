using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : CustomBehaviour
{
    public UIManager UIManager;
    public AudioManager AudioManager;
    public CameraManager CameraManager;
    public EventManager EventManager;
    public LevelManager LevelManager;
    public InputController Inputs;

    public void Awake()
    {
        //Application.targetFrameRate = 60;
        EventManager.Initialize(this);
        UIManager.Initialize(this);
        AudioManager.Initialize(this);
        CameraManager.Initialize(this);
        LevelManager.Initialize(this);
        Inputs.Initialize(this);
    }

    private void Start()
    {
        EventManager.StartGame();
    }
}