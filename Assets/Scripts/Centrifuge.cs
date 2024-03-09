using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Centrifuge : MonoBehaviour
{
    [SerializeField] private Button startButton;

    private static readonly List<TestTube> CurrentTestTubes = new List<TestTube>();
    private bool _isCapClosed;
    private const float CentrifugeDuration = 5.0f;


    private void Awake()
    {
        startButton.onClick.AddListener(StartCentrifuge);
    }
    
    private void StartCentrifuge()
    {
        if (CurrentTestTubes is not null && _isCapClosed)
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
        if(!CurrentTestTubes.Contains(testTube))
        {
            CurrentTestTubes.Add(testTube);
        }
    }
    
    public static void RemoveTestTube(TestTube testTube)
    {
        if (CurrentTestTubes.Contains(testTube))
        {
            CurrentTestTubes.Remove(testTube);
        }
    }

    private IEnumerator CentrifugeTestTube()
    {
        Debug.Log("Centrifugation started");
        yield return new WaitForSeconds(CentrifugeDuration);
        Debug.Log("Centrifugation completed");

        foreach (var testTube in CurrentTestTubes)
        {
            testTube.SetBloodState(BloodState.Centrifuged);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CentrifugeCap"))
        {
            _isCapClosed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
         if (other.CompareTag("CentrifugeCap"))
         {
                _isCapClosed = false;
         }
    }
}

