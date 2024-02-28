using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentrifugeSlot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.TryGetComponent<TestTube>(out var testTube);
        if (other.CompareTag("TestTube"))
        {
            Debug.Log("TRIGGERED: " + other.gameObject.name);
            if (testTube.currentState == TestTube.BloodState.WholeBlood)
            {
                Centrifuge.SetTestTube(testTube);
                other.gameObject.transform.position = transform.position;
                other.gameObject.transform.rotation = transform.rotation;
                Rigidbody rb = other.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TestTube"))
        {
            other.gameObject.TryGetComponent<TestTube>(out var testTube);
            Debug.Log("OnTriggerExit");
            Centrifuge.RemoveTestTube();
        }
    }
}
