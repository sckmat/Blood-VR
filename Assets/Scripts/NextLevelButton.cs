using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour
{
    private void Awake()
    {
        var button = GetComponent<Button>();
        if (LevelManager.instance.currentLevel == 6 || LevelManager.instance.currentMode == LevelMode.FreeAccess)
        {
            button.interactable = false;
        }
        button.onClick.AddListener(NextLevel);
    }

    private void NextLevel()
    {
        LevelManager.instance.SetLevel(LevelMode.Level, LevelManager.instance.currentLevel + 1);
        BloodManager.ClearTestTubes();
        SceneManager.LoadScene("Laboratory");
    }
}
