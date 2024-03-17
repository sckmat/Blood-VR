using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GuideTabManager : MonoBehaviour
{
    [SerializeField] private Transform buttonsParent;
    [SerializeField] private Transform contentParent;

    private Button[] _buttons;
    private GameObject[] _contents;

    private void Start()
    {
        _buttons = buttonsParent.GetComponentsInChildren<Button>();
        _contents = contentParent.Cast<Transform>().Select(t => t.gameObject).ToArray();

        for (int i = 0; i < _buttons.Length; i++)
        {
            int index = i;  // Локальная переменная, необходима для замыкания в лямбда-выражении
            _buttons[i].onClick.AddListener(() => SelectContent(index));
            _contents[i].SetActive(i == 0);  // Активируем первый контент
            _buttons[i].interactable = i != 0;  // Деактивируем первую кнопку
        }
    }

    private void SelectContent(int index)
    {
        for (int i = 0; i < _contents.Length; i++)
        {
            _contents[i].SetActive(i == index);
            _buttons[i].interactable = i != index;
        }
    }
}
