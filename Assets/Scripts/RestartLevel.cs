using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartLevel : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(NextLevel);
    }
    
    private void NextLevel()
    {
        BloodManager.ClearTestTubes();
        SceneManager.LoadScene("Laboratory");
    }
}