using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{ 
    public static LevelManager instance { get; private set; }

    public LevelMode currentMode { get; private set; } = LevelMode.FreeAccess;
    public int currentLevel { get; private set; } = 0;
    public List<BloodSample> levelBloodSample { get; private set; } = new List<BloodSample>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartLevel()
    {
        levelBloodSample = new List<BloodSample>();
        InitializeLevel();
        BloodManager.InitializeTestTubes();
    }

    private void InitializeLevel()
    {
        switch (currentMode)
        {
            case LevelMode.Level:
                LoadLevelState();               
                break;
            case LevelMode.FreeAccess:
                EnableFreeLabAccess();
                break;
        }
    }

    public void SetLevel(LevelMode newState, int level)
    {
        currentMode = newState;
        currentLevel = level;
    }

    private void LoadLevelState()
    {
        switch (currentLevel)
        {
            case 1:
                levelBloodSample.Add(new BloodSample(BloodType.A, RhesusFactor.Negative));
                Debug.Log($"1 load");
                break;
            case 2:
                levelBloodSample.Add(new BloodSample(BloodType.B, RhesusFactor.Negative));
                Debug.Log($"2 load");
                break;
            case 3:
                levelBloodSample.Add(new BloodSample(BloodType.AB, RhesusFactor.Negative));
                Debug.Log($"3 load");
                break;
            case 4:
                levelBloodSample.Add(new BloodSample(BloodType.O, RhesusFactor.Negative));
                Debug.Log($"4 load");
                break;
            case 5:
                levelBloodSample.Add(new BloodSample(BloodType.B, RhesusFactor.Positive));
                levelBloodSample.Add(new BloodSample(BloodType.A, RhesusFactor.Negative));
                Debug.Log($"5 load");
                break;
            case 6:
                Debug.Log($"6 load");
                break;
        }
    }
    
    private void EnableFreeLabAccess()
    {
        Debug.Log($"{currentMode} load");
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Laboratory") return;
        Debug.Log("OnSceneLoaded");
        levelBloodSample = new List<BloodSample>();
        Debug.LogWarning(levelBloodSample.Count);
        InitializeLevel();
        BloodManager.InitializeTestTubes();
    }
    
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

public enum LevelMode
{
    Level,
    FreeAccess
}