using System;
using UnityEngine;

public class CentrifugeSlot : MonoBehaviour
{
    private bool _isEmpty = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!_isEmpty) return;
        other.gameObject.TryGetComponent<TestTube>(out var testTube);
        if (other.CompareTag("TestTube"))
        {
            Debug.Log("TRIGGERED: " + other.gameObject.name);
            if (testTube.currentState == BloodState.WholeBlood)
            {
                Centrifuge.SetTestTube(testTube);
                other.gameObject.transform.position = transform.position;
                other.gameObject.transform.rotation = transform.rotation;
                _isEmpty = false;
                gameObject.GetComponent<Collider>().isTrigger = false;
                Rigidbody rb = other.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                    Debug.Log(gameObject.name + _isEmpty);
                }
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (_isEmpty) return;
        if (other.CompareTag("TestTube"))
        {
            other.gameObject.TryGetComponent<TestTube>(out var testTube);
            gameObject.GetComponent<Collider>().isTrigger = true;
            Debug.Log("OnTriggerExit");
            Centrifuge.RemoveTestTube(testTube);
            _isEmpty = true;
            Debug.Log(gameObject.name + _isEmpty);
        }
    }
}
