using System.Collections;
using System.Collections.Generic;
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
        BloodManager.testTubes.Clear();
        SceneManager.LoadScene("Laboratory");
    }
}