using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ProfileSelector : MonoBehaviour
{
    [SerializeField] private Button selectProfile;
    public Transform contentPanel;
    public GameObject profilePrefab;
    private Button _selectedProfileButton = null;
    private string _selectedProfileName = null;


    void Start()
    {
        LoadProfiles();
        selectProfile.onClick.AddListener(LoadSavedData);
    }

   private void LoadProfiles()
    {
        string profilesPath = Application.persistentDataPath;

        foreach (var directory in Directory.GetDirectories(profilesPath))
        {
            var profileName = new DirectoryInfo(directory).Name;
            var profileItem = Instantiate(profilePrefab, contentPanel);
            var profileText = profileItem.GetComponentInChildren<TMP_Text>();
            profileText.text = profileName;

            var profileButton = profileItem.GetComponent<Button>();
            profileButton.onClick.AddListener(() => SelectProfile(profileName, profileButton));
        }
    }

    private void SelectProfile(string profileName, Button profileButton)
    {
        if (_selectedProfileButton != null)
        {
            _selectedProfileButton.interactable = true;
        }

        _selectedProfileButton = profileButton;
        _selectedProfileName = profileName;
        selectProfile.interactable = true;
        profileButton.interactable = false;

        Debug.Log("Выбран профиль: " + profileName);
    }
    
    private void LoadSavedData()
    {
        if (_selectedProfileName != null)
        {
            UserManager.LoadUser(_selectedProfileName);
        }
    }
}
