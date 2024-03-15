using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartLevel : MonoBehaviour
{
    [SerializeField] private LevelMode levelState;
    [SerializeField] private int level;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(LoadLevel);
        if (levelState == LevelMode.FreeAccess && UserManager.currentUser.statistics.levelsCompleted != 6) return;
        if (levelState == LevelMode.Level && level > UserManager.currentUser.statistics.levelsCompleted + 1) return;
        GetComponent<Button>().interactable = true;
    }

    private void LoadLevel()
    {
        LevelManager.SetLevel(levelState, level);
        SceneManager.LoadScene("Laboratory");
    }
}