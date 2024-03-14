using UnityEngine;

public class CentrifugeSlot : MonoBehaviour
{
    private TestTube _testTube;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TestTube"))
        {
            other.gameObject.TryGetComponent<TestTube>(out var testTube);
            if (testTube.currentState == BloodState.WholeBlood && _testTube == null)
            {
                Debug.Log("TRIGGERED: " + other.gameObject.name);
                _testTube = testTube;
                Centrifuge.SetTestTube(testTube);
                SnapTestTubeToSlot(testTube);
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TestTube"))
        {
            other.gameObject.TryGetComponent<TestTube>(out var testTube);
            if (_testTube == testTube)
            {
                Debug.Log("OnTriggerExit");
                _testTube = null;
                Centrifuge.RemoveTestTube(testTube);
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
