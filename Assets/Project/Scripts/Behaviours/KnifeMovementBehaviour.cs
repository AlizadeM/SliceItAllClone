using JetBrains.Annotations;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMovementBehaviour : MonoBehaviour
{
    public bool IsKnifeDebuffCleared => _isKnifeDebuffCleared;
    public bool IsKnifeStuck { get; set; }

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 _jumpForce;
    [SerializeField] private Vector3 _jumpForceY;
    [SerializeField] private Vector3 _rotateForce;

    private KnifeController _knifeController;
    private bool _isKnifeDebuffCleared;
    private bool _isKnifeSlicingTower;
    private bool _isKnifeSlicingDebuffCleared;

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

        if (_isKnifeSlicingTower && !_isKnifeSlicingDebuffCleared)
        {
            rb.angularVelocity = _rotateForce / 15 * Time.deltaTime;
        }

        //Slows down knife on ideal rotation
        if (rb.angularVelocity.z < -3)
        {
            if ((gameObject.transform.localEulerAngles.z >= 0 && gameObject.transform.localEulerAngles.z <= 90) && !_isKnifeDebuffCleared)
            {
                rb.AddTorque((-_rotateForce * 3) * Time.deltaTime, ForceMode.Acceleration);
            }
        }
    }

    private void KnifeJumpMovement()
    {
        if (IsKnifeStuck)
        {
            StartCoroutine(KnifeUnstuck());
        }

        if (rb.velocity.x < 10f)
        {
            rb.AddForce(_jumpForce, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(_jumpForceY, ForceMode.Impulse);
        }
    }

    public void KnifeJumpBackwardsMovement()
    {
        rb.AddForce(new Vector3(-_jumpForce.x * 10, _jumpForce.y * 40, 0) * Time.deltaTime, ForceMode.Impulse);
    }

    public void RotateKnife()
    {
        rb.AddTorque(_rotateForce, ForceMode.Acceleration);
    }

    public void RotateKnifeAfterCollision()
    {
        rb.AddTorque(_rotateForce / 5f, ForceMode.Acceleration);
    }

    public void KnifeStuckCo()
    {
        if (!IsKnifeStuck)
        {
            rb.isKinematic = true;
            IsKnifeStuck = true;
        }
    }

    private void StartClearKnifeSlowDownDebuff()
    {
        StartCoroutine(ClearKnifeSlowDownDebuff());
    }

    public void KnifeTowerSlicingMovement()
    {
        _isKnifeSlicingTower = true;
    }

    public void KnifeTowerSlicingMovementEnded()
    {
        _isKnifeSlicingTower = false;
    }

    private void StartClearKnifeSlicingDebuff()
    {
        StartCoroutine(ClearKnifeTowerSlicingDebuff());
    }

    private IEnumerator ClearKnifeTowerSlicingDebuff()
    {
        _isKnifeSlicingDebuffCleared = true;
        yield return new WaitForSeconds(4f);
        _isKnifeSlicingDebuffCleared = false;
    }

    private IEnumerator KnifeUnstuck()
    {

        rb.isKinematic = false;
        _knifeController._knifeTipCollisionBehaviour.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        IsKnifeStuck = false;
        _knifeController._knifeTipCollisionBehaviour.GetComponent<Collider>().enabled = true;
    }

    private IEnumerator ClearKnifeSlowDownDebuff()
    {
        if (gameObject.transform.localEulerAngles.z >= 0 && gameObject.transform.localEulerAngles.z <= 90)
        {
            _isKnifeDebuffCleared = true;
            yield return new WaitForSeconds(0.5f);
            _isKnifeDebuffCleared = false;
        }

    }

}
