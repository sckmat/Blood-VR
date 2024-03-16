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
        nameInputField.onValueChanged.AddListener(ValidateInput);
        saveButton.onClick.AddListener(CreateAndSaveUser);
        saveButton.interactable = false;
    }

    private void ValidateInput(string input)
    {
        saveButton.interactable = !string.IsNullOrWhiteSpace(input);
    }

    private void CreateAndSaveUser()
    {
        if (!string.IsNullOrWhiteSpace(nameInputField.text))
            UserManager.CreateUser(nameInputField.text);
    }
}
