using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using TMPro;


public class CreateUser : MonoBehaviour
{ 
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button saveButton;

    private void Start()
    {
        saveButton.onClick.AddListener(CreateAndSaveUser);
    }

    private void CreateAndSaveUser()
    {
        UserManager.CreateUser(nameInputField.text);
    }
}
