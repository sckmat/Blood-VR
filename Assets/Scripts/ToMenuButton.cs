using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToMenuButton : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(LoadMenu);
    }

    private void LoadMenu()
    {
        BloodManager.ClearTestTubes();
        SceneManager.LoadScene("Start");
    }
}