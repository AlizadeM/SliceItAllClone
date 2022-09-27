using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputController : CustomBehaviour
{
    public static bool IsInputDeactivated { get; set; } // All gameplay input depends on this, close from UI
    public bool IsTouching { get; private set; }

    public event Action OnMouseDown;


    #region Initialize

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
    }

    private void OnDestroy()
    {
    }

    #endregion

    private void Update()
    {
        if (!IsInputDeactivated)
        {

            if (Input.GetMouseButtonDown(0))
                OnMouseDown?.Invoke();
        }
    }
}
