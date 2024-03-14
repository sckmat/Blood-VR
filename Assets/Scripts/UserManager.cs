using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public static class UserManager
{
    public static User currentUser { get; set; }

    public static void CreateUser(string nickname)
    {
        currentUser = new User(nickname);
        SaveCurrentUser();
    }

    public static void LoadUser(string json)
    {
        currentUser = JsonConvert.DeserializeObject<User>(json);
    }

    private static void SaveCurrentUser()
    {
        SaveManager.instance.SaveData(currentUser);
    }
}