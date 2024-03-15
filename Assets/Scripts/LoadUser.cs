using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadUser : MonoBehaviour
{
    [SerializeField] private Button saveButton;

    private void Start()
    {
        saveButton.onClick.AddListener(CreateAndSaveUser);
    }

    private void CreateAndSaveUser()
    {
        UserManager.LoadUser();
    }
}
