using UnityEngine;
using UnityEngine.UI;

public class TestTubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject testTubePrefab; 
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Button spawn;

    private void Awake()
    {
        gameObject.SetActive(LevelManager.instance.currentMode == LevelMode.FreeAccess || LevelManager.instance.currentLevel == 6);
        spawn.onClick.AddListener(SpawnTestTube);
    }

    private void Update()
    {
        spawn.interactable = BloodManager.testTubes.Count <= 0;
    }

    public void SpawnTestTube()
    {
        Instantiate(testTubePrefab, spawnPoint.position, spawnPoint.rotation);
        BloodManager.InitializeTestTubes();
    }
}