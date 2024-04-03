using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartLevel : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Restart);
    }
    
    private void Restart()
    {
        BloodManager.ClearTestTubes();
        SceneManager.LoadScene("Laboratory");
    }
}