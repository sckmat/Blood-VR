using UnityEngine;

public class CurrentTestTube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TestTube"))
        {
            var testTube = other.GetComponent<TestTube>();
            if (testTube.currentState == BloodState.Centrifuged)
            {
                SnapTestTubeToSlot(testTube);
                BloodManager.SetCurrentTestTube(testTube);
                Debug.Log("SetCurrentTestTube" + gameObject.name);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TestTube"))
        {
            BloodManager.RemoveCurrentTestTube();
            Debug.Log("RemoveCurrentTestTube" + gameObject.name);
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
