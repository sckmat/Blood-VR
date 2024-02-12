using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centrifuge : MonoBehaviour
{
    private TestTube _currentTestTube;
    private const float CentrifugeDuration = 5.0f;

    private void OnTriggerEnter(Collider other)
    {
        var testTube = other.gameObject.GetComponent<TestTube>();
        if (testTube == null) return;
        StartCoroutine(CentrifugeTestTube(testTube));
        Debug.Log("Start");
    }
    
    private IEnumerator CentrifugeTestTube(TestTube testTube)
    {
        yield return new WaitForSeconds(CentrifugeDuration);
        Debug.Log("Cent");
        if (testTube != null)
        {
            testTube.SetBloodState(TestTube.BloodState.Centrifuged);
        }
    }
}

