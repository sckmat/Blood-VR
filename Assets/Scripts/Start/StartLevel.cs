using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StartLevel : MonoBehaviour
{
    [SerializeField] private LevelMode levelState;
    [SerializeField] private int level;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(LoadLevel);
    }

    private void LoadLevel()
    {
        LevelManager.SetLevel(levelState, level);
        SceneManager.LoadScene("Laboratory");
    }
}
