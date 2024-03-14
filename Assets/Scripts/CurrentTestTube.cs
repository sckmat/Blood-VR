using UnityEngine;

public class CurrentTestTube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TestTube"))
        {
            var testTube = other.GetComponent<TestTube>();
            if (BloodManager.currentTestTube == null)
            {
                SnapTestTubeToSlot(testTube);
                BloodManager.SetCurrentTestTube(testTube);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TestTube"))
        {
            var testTube = other.GetComponent<TestTube>();
            if (testTube == BloodManager.currentTestTube)
            {
                BloodManager.RemoveCurrentTestTube();
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
