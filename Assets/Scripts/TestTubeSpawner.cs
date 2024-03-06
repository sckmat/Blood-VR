using UnityEngine;

public class TestTubeSpawner : MonoBehaviour
{
    public GameObject testTubePrefab; 
    public Transform spawnPoint;

    public void SpawnTestTube()
    {
        Instantiate(testTubePrefab, spawnPoint.position, spawnPoint.rotation);
        BloodManager.InitializeTestTubes();
    }
}