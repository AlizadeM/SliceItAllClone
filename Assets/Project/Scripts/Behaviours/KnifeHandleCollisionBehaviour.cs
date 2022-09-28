using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeHandleCollisionBehaviour : MonoBehaviour
{
    private KnifeController _knifeController;

    [SerializeField] KnifeMovementBehaviour m_Behaviour;

    public void Initialize(KnifeController knifeController)
    {
        _knifeController = knifeController;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "SliceableObject")
        {
            Debug.Log("Collision");
            if (!_knifeController._knifeMovementBehaviour.IsKnifeStuck)
            {
                _knifeController._knifeMovementBehaviour.KnifeJumpBackwardsMovement();
                _knifeController._knifeMovementBehaviour.RotateKnifeAfterCollision();
            }
        }
    }

}
