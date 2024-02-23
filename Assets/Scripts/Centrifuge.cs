using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Centrifuge : MonoBehaviour
{
   // [SerializeField] private GameObject centrifugeSlot;
    private static TestTube _currentTestTube;
    private static bool _isCentrifugeReady = false;
    public static bool isCentrifuged = false;
    private const float CentrifugeDuration = 5.0f;

    private void Update()
    {
        if (_isCentrifugeReady && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(CentrifugeTestTube());
            Debug.Log("Start Centrifuging");
        }
    }
    
    public static void SetTestTube(TestTube testTube)
    {
        _currentTestTube = testTube;
        _isCentrifugeReady = true;
        isCentrifuged = false;
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
        isCentrifuged = true;
    }
}

