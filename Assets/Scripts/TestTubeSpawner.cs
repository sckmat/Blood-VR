using System;
using UnityEngine;

public class TestTubeSpawner : MonoBehaviour
{
    public GameObject testTubePrefab; 
    public Transform spawnPoint;

    private void Awake()
    {
        gameObject.SetActive(LevelManager.currentMode != LevelMode.Level);
    }

    public void SpawnTestTube()
    {
        if(BloodManager.testTubes.Count > 0) return;
        Instantiate(testTubePrefab, spawnPoint.position, spawnPoint.rotation);
        BloodManager.InitializeTestTubes();
    }
}