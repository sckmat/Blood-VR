using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandSlot : MonoBehaviour
{
    private TestTube _testTube;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TestTube") && _testTube == null)
        {
            var testTube = other.GetComponent<TestTube>();
            _testTube = testTube;
            SnapTestTubeToSlot(testTube);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TestTube"))
        {
            var testTube = other.GetComponent<TestTube>();
            if (testTube == _testTube)
            {
                _testTube = null;
            }
        }
    }

    private void SnapTestTubeToSlot(TestTube testTube)
    {
        testTube.transform.position = transform.position;
        testTube.transform.rotation = transform.rotation;
        Rigidbody rb = testTube.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }
}
