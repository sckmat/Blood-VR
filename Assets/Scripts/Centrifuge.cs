using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Centrifuge : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private TMP_Text countdownText;

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
            countdownText.gameObject.SetActive(true);
            GetComponent<AudioSource>().Play();
            StartCoroutine(CentrifugeTestTube());
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

    public static bool CheckTestTube(TestTube testTube)
    {
        return CurrentTestTubes.Contains(testTube);
    }
    
    private IEnumerator CentrifugeTestTube()
    {
        Debug.Log("Centrifugation started");
        yield return StartCoroutine(Countdown(CentrifugeDuration));
        Debug.Log("Centrifugation completed");

        foreach (var testTube in CurrentTestTubes)
        {
            testTube.SetBloodState(BloodState.Centrifuged);
        }
    }

    private IEnumerator Countdown(float duration)
    {
        var timeLeft = duration;
        while (timeLeft > 0)
        {
            countdownText.text = $"{timeLeft:F1}с.";
            yield return new WaitForSeconds(0.1f);
            timeLeft -= 0.1f;
        }
        countdownText.text = "0.0с.";
        GetComponent<AudioSource>().Stop();
        AudioManager.instance.PlayDoneSoundAtPosition(transform.position);
        countdownText.gameObject.SetActive(false);
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

