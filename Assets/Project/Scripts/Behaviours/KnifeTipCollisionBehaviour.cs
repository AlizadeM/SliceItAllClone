using MangoramaStudio.Scripts.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class KnifeTipCollisionBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask _detectionLayerMask;
    private KnifeController _knifeController;
    private GameObject _currentTower;
    private MultiplierCubeBehaviour _currentMultiplier;
    public void Initialize(KnifeController knifeController)
    {
        _knifeController = knifeController;
        StartCoroutine(DetectCo());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ground"))
        {
            _knifeController._knifeMovementBehaviour.KnifeStuckCo();
        }

        if (other.CompareTag("Multiplier"))
        {
            if (_currentMultiplier == null)
            {
                _knifeController._knifeMovementBehaviour.KnifeStuckCo();
                _currentMultiplier = other.GetComponent<MultiplierCubeBehaviour>();
                _knifeController.MultiplierCubeReached(_currentMultiplier);
                
            }
        }

        if (other.CompareTag("OutOfBounds") || other.CompareTag("Trap"))
        {
            _knifeController.LevelFailed();
        }

        if (other.CompareTag("Finish"))
        {
            _knifeController.LevelCompletedWithoutMultiplier();
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    //if (other.CompareTag("Tower"))
    //    //{
    //    //    _knifeController._knifeMovementBehaviour.KnifeTowerSlicingMovementEnded();
    //    //}
    //}

    private IEnumerator DetectCo()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            Collider[] towers = Physics.OverlapSphere(transform.position, 2f, _detectionLayerMask);


            if (towers.Length > 0)
            {
                if (!_currentTower)
                {
                    //if (_knifeController._knifeMovementBehaviour.IsKnifeDebuffCleared)
                    //    break;
                    _currentTower = towers[0].gameObject;
                    _knifeController._knifeMovementBehaviour.KnifeTowerSlicingMovement();
                }
            }

            else
            {
                if (_currentTower)
                {
                    _currentTower = null;
                    _knifeController._knifeMovementBehaviour.KnifeTowerSlicingMovementEnded();
                }
            }
        }
    }


}
