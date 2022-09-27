using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeTipCollisionBehaviour : MonoBehaviour
{
    private KnifeController _knifeController;
    public void Initialize(KnifeController knifeController)
    {
        _knifeController = knifeController;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ground"))
        { 
            _knifeController._knifeMovementBehaviour.KnifeStuckCo();
        }

    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("ground"))
    //    {
    //        _knifeController._knifeMovementBehaviour.KnifeUnstuck();
    //    }
    //}


}
