using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandSlot : MonoBehaviour
{
    private bool _isEmpty = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TestTube") && _isEmpty)
        {
            var testTube = other.GetComponent<TestTube>();
            SnapTestTubeToSlot(testTube);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TestTube") && !_isEmpty)
        {
            _isEmpty = true;
        }
    }

    private void SnapTestTubeToSlot(TestTube testTube)
    {
        testTube.transform.position = transform.position;
        testTube.transform.rotation = transform.rotation;
        _isEmpty = false;
        Rigidbody rb = testTube.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }
}
