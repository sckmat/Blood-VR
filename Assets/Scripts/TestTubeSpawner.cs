using UnityEngine;

public class TestTubeSpawner : MonoBehaviour
{
    public GameObject testTubePrefab; 
    public Transform spawnPoint;

    public void SpawnTestTube()
    {
        if(BloodManager.testTubes.Count > 0) return;
        Instantiate(testTubePrefab, spawnPoint.position, spawnPoint.rotation);
        BloodManager.InitializeTestTubes();
    }
}