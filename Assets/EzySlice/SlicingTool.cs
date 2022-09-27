using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using System;

public class SlicingTool : MonoBehaviour
{

    public Material materialSlicedSide;
    public float explosionForce;
    public float exposionRadius;
    public bool gravity, kinematic;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SliceableObject"))
        {
            SlicedHull sliceobj = Slice(other.gameObject, materialSlicedSide);
            GameObject SlicedobjTop = sliceobj.CreateUpperHull(other.gameObject, materialSlicedSide);
            GameObject SliceObjDown = sliceobj.CreateLowerHull(other.gameObject, materialSlicedSide);
            Destroy(other.gameObject);
            AddComponent(SlicedobjTop);
            AddComponent(SliceObjDown);
        }
    }

    private SlicedHull Slice(GameObject obj, Material mat)
    {
        return obj.Slice(transform.position, direction: transform.up, mat);
    }

    void AddComponent(GameObject obj)
    {
        obj.AddComponent<BoxCollider>();
        var rigidbody = obj.AddComponent<Rigidbody>();
        rigidbody.useGravity = gravity;
        rigidbody.isKinematic = kinematic;
        rigidbody.AddExplosionForce(explosionForce, obj.transform.position, exposionRadius);
        // Destroy(obj,3f);
    }

}

