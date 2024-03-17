using System;
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
        var json = JsonConvert.SerializeObject(user);
        var userFolderPath = Path.Combine(Application.persistentDataPath, user.nickname);
        if (!Directory.Exists(userFolderPath))
            Directory.CreateDirectory(userFolderPath);
        var filePath = Path.Combine(userFolderPath, "save.json");
        File.WriteAllText(filePath, json);
        Debug.Log($"Data saved to {filePath}");
    }

    public User LoadData(string nickname)
    {
        var path = Path.Combine(Application.persistentDataPath, $"{nickname}/save.json");
        try
        {
            var json = File.ReadAllText(path);
            Debug.Log(json);
            var x = JsonConvert.DeserializeObject<User>(json);
            Debug.LogWarning(x.nickname + x.statistics.levelsCompleted + x.statistics.bloodGroupStatistics.Count);
            return JsonConvert.DeserializeObject<User>(json);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error loading user data: " + ex.Message);
        }

        return null;
    }
}