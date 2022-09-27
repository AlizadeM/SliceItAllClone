using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputController : CustomBehaviour
{
    public static bool IsInputDeactivated { get; set; } // All gameplay input depends on this, close from UI

    public static event Action OnPressPerformed;
    public static event Action<Vector2> OnDrag; // This can be used as a input value, if x > 0, player swiped right, ex.
    public static event Action OnPressCancelled;

    private Vector2 _firstPosition;
    private Vector2 _lastPosition;
    private Vector2 _dragVector;

    private bool _isDragging;


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
            {
                _firstPosition = Input.mousePosition;
                OnPressPerformed?.Invoke();
                _isDragging = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _lastPosition = Input.mousePosition;
                OnPressCancelled?.Invoke();
                _isDragging = false;
            }

            if (_isDragging)
            {
                _dragVector = (Vector2)Input.mousePosition - _firstPosition;

                OnDrag?.Invoke(_dragVector * 0.02f);

                _firstPosition = Input.mousePosition;
            }
        }
    }
}
