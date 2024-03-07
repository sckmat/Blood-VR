using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelMode currentMode { get; private set; } = LevelMode.FreeAccess;
    private static int _currentLevel = 0;
    public static LevelState levelState { get; private set; }

    void Start()
    {
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
        _currentLevel = level;
    }

    void LoadLevelState()
    {
        switch (_currentLevel)
        {
            case 1:
                levelState = new LevelOne();
                Debug.Log($"1 load");
                break;
        }
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