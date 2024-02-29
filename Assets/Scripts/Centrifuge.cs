using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Centrifuge : MonoBehaviour
{
    [SerializeField] private Button startButton;

    private static TestTube _currentTestTube;
    private bool _isCapClosed;
    private const float CentrifugeDuration = 5.0f;


    private void Awake()
    {
        startButton.onClick.AddListener(StartCentrifuge);
    }
    
    private void StartCentrifuge()
    {
        if (_currentTestTube is not null && _isCapClosed)
        {
            StartCoroutine(CentrifugeTestTube());
            Debug.Log("Start Centrifuging");
        }
        else
        {
            Debug.Log("Пробирки нет или крышка не закрыта");
        }
    }
    
    public static void SetTestTube(TestTube testTube)
    {
        _currentTestTube = testTube;
    }
    
    public static void RemoveTestTube()
    {
        _currentTestTube = null;
    }

    private IEnumerator CentrifugeTestTube()
    {
        Debug.Log("Centrifugation started");
        yield return new WaitForSeconds(CentrifugeDuration);
        Debug.Log("Centrifugation completed");
    
        _currentTestTube.SetBloodState(BloodState.Centrifuged);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CentrifugeCap"))
        {
            _isCapClosed = true;
            Debug.Log("_isCapClosed true");
        }
    }

    private void OnTriggerExit(Collider other)
    {
         if (other.CompareTag("CentrifugeCap"))
         {
                _isCapClosed = false;
                Debug.Log("_isCapClosed false");
         }
    }
}

