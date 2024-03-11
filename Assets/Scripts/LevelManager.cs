using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelMode currentMode { get; private set; } = LevelMode.FreeAccess;
    public static int currentLevel { get; private set; } = 0;
    public static List<BloodSample> levelBloodSample { get; private set; } = new List<BloodSample>();

    void Start()
    {
        levelBloodSample = new List<BloodSample>();
        InitializeLevel();
        BloodManager.InitializeTestTubes();
    }

    void InitializeLevel()
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

    public static void SetLevel(LevelMode newState, int level)
    {
        currentMode = newState;
        currentLevel = level;
    }

    void LoadLevelState()
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

    public static void RemoveLevelBloodSample(BloodSample bloodSample)
    {
        levelBloodSample.Remove(bloodSample);
    }
    
    void EnableFreeLabAccess()
    {
        Debug.Log($"{currentMode} load");
    }
}

public enum LevelMode
{
    Level,
    FreeAccess
}