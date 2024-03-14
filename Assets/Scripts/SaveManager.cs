using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { get; private set; }
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void SaveData(User user)
    {
        string json = JsonConvert.SerializeObject(user);
        Debug.Log(user.bloodGroupStatistics.Keys);
        string path = Path.Combine(Application.persistentDataPath, "user.json");
        File.WriteAllText(path, json);
        Debug.Log($"Data saved to {path}");
    }
}

