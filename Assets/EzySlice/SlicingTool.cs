using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using System;
using MangoramaStudio.Scripts.Data;
using Random = UnityEngine.Random;

public class SlicingTool : MonoBehaviour
{

    public event Action OnObjectSliced;

    [SerializeField] private List<Material> _materialSlicedSide;
    [SerializeField] private float _explosionForce;
    [SerializeField] private bool _gravity, _kinematic;
    [SerializeField] private LayerMask _slicedObjectLayerMask;

    private KnifeController _knifeController;


    public void Initialize(KnifeController knifeController)
    {
        _knifeController = knifeController;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SliceableObject"))
        {
            SlicedHull sliceobj = Slice(other.gameObject, _materialSlicedSide[Random.Range(0, _materialSlicedSide.Count - 1)]);
            GameObject SlicedobjTop = sliceobj.CreateUpperHull(other.gameObject, _materialSlicedSide[Random.Range(0, _materialSlicedSide.Count - 1)]);
            GameObject SliceObjDown = sliceobj.CreateLowerHull(other.gameObject, _materialSlicedSide[Random.Range(0, _materialSlicedSide.Count - 1)]);
            Destroy(other.gameObject);
            AddComponent(SlicedobjTop, -1);
            AddComponent(SliceObjDown, 1);
        }
    }

    private SlicedHull Slice(GameObject obj, Material mat)
    {
        OnObjectSliced?.Invoke();
        return obj.Slice(transform.position, direction: transform.up, mat);
    }

    void AddComponent(GameObject obj, int direction)
    {
        obj.AddComponent<BoxCollider>();
        var rigidbody = obj.AddComponent<Rigidbody>();
        rigidbody.useGravity = _gravity;
        rigidbody.isKinematic = _kinematic;
        rigidbody.AddForce(Vector3.forward * _explosionForce * direction, ForceMode.Impulse);
        obj.transform.parent = _knifeController.GameManager.LevelManager.CurrentLevel.transform;
        obj.layer = _slicedObjectLayerMask;
    }

}

