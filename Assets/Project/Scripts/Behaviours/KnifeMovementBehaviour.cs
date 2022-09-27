using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMovementBehaviour : MonoBehaviour
{
    public bool IsKnifeStuck { get; set; }

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 _jumpForce;
    [SerializeField] private Vector3 _rotateForce;

    private KnifeController _knifeController;

    public void Initialize(KnifeController knifeController)
    {
        _knifeController = knifeController;
        _knifeController.GameManager.Inputs.OnMouseDown += KnifeJumpMovement;
        _knifeController.GameManager.Inputs.OnMouseDown += RotateKnife;
    }

    private void Update()
    {
        //if (_knifeController.GameManager.Inputs.IsTouching)
        //{
        //    if (IsKnifeStuck)
        //    {
        //        StartCoroutine(KnifeUnstuck());
        //    }

        //    KnifeJumpMovement();
        //    RotateKnife();
        //}
    }

    public void KnifeJumpMovement()
    {
        if (IsKnifeStuck)
        {
            StartCoroutine(KnifeUnstuck());
        }
        rb.AddForce(_jumpForce, ForceMode.Impulse);
    }

    public void KnifeJumpBackwardsMovement()
    {
        rb.AddForce(new Vector3(-_jumpForce.x, _jumpForce.y, 0), ForceMode.Impulse);
    }

    [Button]
    public void RotateKnife()
    {
        rb.AddTorque(_rotateForce, ForceMode.Acceleration);
    }

    public void KnifeStuckCo()
    {
        if (!IsKnifeStuck)
        {
            rb.isKinematic = true;
            IsKnifeStuck = true;
        }
    }

    public IEnumerator KnifeUnstuck()
    {

        rb.isKinematic = false;
        _knifeController._knifeTipCollisionBehaviour.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(0.7f);
        IsKnifeStuck = false;
        _knifeController._knifeTipCollisionBehaviour.GetComponent<Collider>().enabled = true;


    }


}
