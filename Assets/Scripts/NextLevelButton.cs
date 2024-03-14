using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour
{
    private void Awake()
    {
        var button = GetComponent<Button>();
        if (LevelManager.currentLevel == 6 || LevelManager.currentMode == LevelMode.FreeAccess)
        {
            button.interactable = false;
        }
        button.onClick.AddListener(NextLevel);
    }

    private void NextLevel()
    {
        LevelManager.SetLevel(LevelMode.Level, LevelManager.currentLevel + 1);
        BloodManager.testTubes.Clear();
        SceneManager.LoadScene("Laboratory");
    }
}
