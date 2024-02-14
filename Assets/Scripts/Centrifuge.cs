using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Centrifuge : MonoBehaviour
{
    [SerializeField] private GameObject centrifugeSlot;
    private static TestTube _currentTestTube;
    private static bool _isCentrifugeReady = false;
    private const float CentrifugeDuration = 5.0f;

    private void Update()
    {
        if (centrifugeSlot && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(CentrifugeTestTube());
            Debug.Log("Start Centrifuging");
        }
    }
    
    public static void SetTestTube(TestTube testTube)
    {
        _currentTestTube = testTube;
        _isCentrifugeReady = true;
    }
    
    public static void RemoveTestTube()
    {
        _currentTestTube = null;
        _isCentrifugeReady = false;
    }

    private IEnumerator CentrifugeTestTube()
    {
        Debug.Log("Centrifugation started");
        yield return new WaitForSeconds(CentrifugeDuration);
        Debug.Log("Centrifugation completed");

        _currentTestTube.SetBloodState(TestTube.BloodState.Centrifuged);
    }
    
    // private void OnTriggerEnter(Collider other)
    // {
    //     var testTube = other.gameObject.GetComponent<TestTube>();
    //     if (testTube == null) return;
    //     StartCoroutine(CentrifugeTestTube(testTube));
    //     Debug.Log("Start");
    // }
    //
    // private IEnumerator CentrifugeTestTube(TestTube testTube)
    // {
    //     yield return new WaitForSeconds(CentrifugeDuration);
    //     Debug.Log("Cent");
    //     if (testTube != null)
    //     {
    //         testTube.SetBloodState(TestTube.BloodState.Centrifuged);
    //     }
    // }
}

