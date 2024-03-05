using UnityEngine;

public class CurrentTestTube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TestTube"))
        {
            BloodManager.SetCurrentTestTube(other.GetComponent<TestTube>());
            Debug.Log("SetCurrentTestTube");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TestTube"))
        {
            BloodManager.RemoveCurrentTestTube();
            Debug.Log("RemoveCurrentTestTube");
        }
    }
}
