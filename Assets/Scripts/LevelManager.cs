using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelState currentState = LevelState.Level;

    void Start()
    {
        InitializeLevel();
        BloodManager.InitializeTestTubes();
    }

    void InitializeLevel()
    {
        switch (currentState)
        {
            case LevelState.Level:
                EnableLevel();
                break;
            case LevelState.FreeAccess:
                EnableFreeLabAccess();
                break;
        }
    }

    public void SetLevelState(LevelState newState)
    {
        currentState = newState;
        InitializeLevel();
    }

    void EnableLevel()
    {
        // todo  уровни
    }
    
    void EnableFreeLabAccess()
    {
        // todo свободный доступ
    }
}

public enum LevelState
{
    Level,
    FreeAccess
}
