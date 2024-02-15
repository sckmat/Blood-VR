using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentrifugeSlot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TestTube") && !Centrifuge.isCentrifuged)
        {
            Debug.Log("OnTriggerEnter");
            Debug.Log("TRIGGERED: " + other.gameObject.name);
            Centrifuge.SetTestTube(other.gameObject.GetComponent<TestTube>());
            other.gameObject.transform.position = transform.position;
            other.gameObject.transform.rotation = transform.rotation;
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TestTube") && Centrifuge.isCentrifuged)
        {
            Debug.Log("OnTriggerExit");
            Centrifuge.RemoveTestTube();
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            { 
                rb.isKinematic = false;
            }
        }
    }
}
