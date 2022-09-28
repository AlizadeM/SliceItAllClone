using JetBrains.Annotations;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMovementBehaviour : MonoBehaviour
{
    public bool IsKnifeStuck { get; set; }

    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Vector3 _jumpForce;
    [SerializeField] private Vector3 _jumpForceY;
    [SerializeField] private Vector3 _rotateForce;

    private Coroutine _knifeSlicingDebuffRoutine;
    private Coroutine _knifeSlowDownDebuffRoutine;
    private KnifeController _knifeController;
    private bool _isKnifeSlicingDebuffCleared;
    private bool _isKnifeDebuffCleared;
    private bool _isKnifeSlicingTower;
    private float _maxKnifeSpeed = 70;

    public void Initialize(KnifeController knifeController)
    {
        _knifeController = knifeController;
        _knifeController.GameManager.Inputs.OnMouseDown += StartClearKnifeSlowDownDebuff;
        _knifeController.GameManager.Inputs.OnMouseDown += StartClearKnifeSlicingDebuff;
        _knifeController.GameManager.Inputs.OnMouseDown += KnifeJumpMovement;
        _knifeController.GameManager.Inputs.OnMouseDown += RotateKnife;
    }

    private void OnDestroy()
    {
        _knifeController.GameManager.Inputs.OnMouseDown -= StartClearKnifeSlicingDebuff;
        _knifeController.GameManager.Inputs.OnMouseDown -= StartClearKnifeSlowDownDebuff;
        _knifeController.GameManager.Inputs.OnMouseDown -= KnifeJumpMovement;
        _knifeController.GameManager.Inputs.OnMouseDown -= RotateKnife;
    }

    private void Update()
    {
        //Positions knife on SliceableObject Tower

        if (_isKnifeSlicingTower && !_isKnifeSlicingDebuffCleared)
        {
            if (gameObject.transform.localEulerAngles.z >= 0 && gameObject.transform.localEulerAngles.z <= 50)
                _rigidBody.angularVelocity = _rotateForce / 15 * Time.deltaTime;
        }

        //Slows down knife on ideal rotation
        if (_rigidBody.angularVelocity.z < -2.5)
        {
            if ((gameObject.transform.localEulerAngles.z >= 0 && gameObject.transform.localEulerAngles.z <= 90) && !_isKnifeDebuffCleared)
            {
                _rigidBody.AddTorque((-_rotateForce * 4) * Time.deltaTime, ForceMode.Acceleration);
            }
        }

        if (_rigidBody.velocity.magnitude > _maxKnifeSpeed)
        {
            _rigidBody.velocity = Vector3.ClampMagnitude(_rigidBody.velocity, _maxKnifeSpeed);
        }
    }

    private void KnifeJumpMovement()
    {
        if (IsKnifeStuck)
        {
            StartCoroutine(KnifeUnstuckCo());
        }


        if (_rigidBody.velocity.x < 10f && _rigidBody.velocity.y < 70)
        {
            _rigidBody.AddForce(_jumpForce, ForceMode.Impulse);
        }

        else if (_rigidBody.velocity.x > 10f && _rigidBody.velocity.y < 70)
        {
            _rigidBody.AddForce(_jumpForceY, ForceMode.Impulse);
        }

        else if (_rigidBody.velocity.x < 10f && _rigidBody.velocity.y > 70)
        {
            _rigidBody.AddForce(new Vector3(_jumpForce.x, 0, 0), ForceMode.Impulse);
        }
    }

    public void KnifeJumpBackwardsMovement()
    {
        _rigidBody.AddForce(new Vector3(-_jumpForce.x * 20, _jumpForce.y * 60, 0) * Time.deltaTime, ForceMode.Impulse);
    }

    public void RotateKnife()
    {
        _rigidBody.AddTorque(_rotateForce, ForceMode.Acceleration);
    }

    public void RotateKnifeAfterCollision()
    {
        _rigidBody.AddTorque(_rotateForce / 5f, ForceMode.Acceleration);
    }

    public void KnifeStuckCo()
    {
        if (!IsKnifeStuck)
        {
            _rigidBody.isKinematic = true;
            IsKnifeStuck = true;
        }
    }


    public void KnifeTowerSlicingMovement()
    {
        _isKnifeSlicingTower = true;
    }

    public void KnifeTowerSlicingMovementEnded()
    {
        _isKnifeSlicingTower = false;
    }
    private void StartClearKnifeSlowDownDebuff()
    {
        if (_knifeSlowDownDebuffRoutine == null)
        {
            _knifeSlowDownDebuffRoutine = StartCoroutine(ClearKnifeSlowDownDebuffCo());
        }
    }

    private void StartClearKnifeSlicingDebuff()
    {
        if (_knifeSlicingDebuffRoutine == null)
        {
            _knifeSlicingDebuffRoutine = StartCoroutine(ClearKnifeTowerSlicingDebuffCo());
        }
    }

    private IEnumerator ClearKnifeTowerSlicingDebuffCo()
    {
        if (_isKnifeSlicingTower)
        {
            _isKnifeSlicingDebuffCleared = true;
            yield return new WaitForSeconds(1f);
            _isKnifeSlicingDebuffCleared = false;
            _knifeSlicingDebuffRoutine = null;
        }
    }

    private IEnumerator KnifeUnstuckCo()
    {
        _rigidBody.isKinematic = false;
        _knifeController._knifeTipCollisionBehaviour.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(0.4f);
        IsKnifeStuck = false;
        _knifeController._knifeTipCollisionBehaviour.GetComponent<Collider>().enabled = true;
    }

    private IEnumerator ClearKnifeSlowDownDebuffCo()
    {
        _isKnifeDebuffCleared = true;
        yield return new WaitForSeconds(0.4f);
        _isKnifeDebuffCleared = false;
        _knifeSlowDownDebuffRoutine = null;
    }

}
