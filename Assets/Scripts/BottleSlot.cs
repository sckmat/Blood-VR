using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BottleSlot : MonoBehaviour
{
    private bool _inSlot;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BottlePipette") && !_inSlot)
        {
            Physics.IgnoreCollision(other.GetComponent<MeshCollider>(), GetComponentInParent<MeshCollider>());
            Debug.Log("TRIGGERED: " + other.gameObject.name);
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
            other.gameObject.transform.position = transform.position;
            other.gameObject.transform.rotation = transform.rotation;
            _inSlot = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BottlePipette"))
        {
            _inSlot = false;
        }
    }
}
