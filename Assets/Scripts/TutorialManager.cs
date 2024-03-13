using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorial1;
    [SerializeField] private GameObject tutorial2;
    [SerializeField] private GameObject tutorial3;
    [SerializeField] private GameObject tutorial4;
    [SerializeField] private GameObject tutorial5;
    [SerializeField] private GameObject tutorial6;

    private Transform _currentHints;
    private int _currentHintIndex = 0;

    private void Awake()
    {
        _currentHints = GetCurrentHints();
        _currentHints.GetChild(0).gameObject.SetActive(true);
        _currentHintIndex++;
    }

    public void ShowNextHint()
    {
        if (_currentHintIndex < _currentHints.childCount)
        {
            if (_currentHintIndex > 0)
            {
                _currentHints.GetChild(_currentHintIndex - 1).gameObject.SetActive(false);
            }
            _currentHints.GetChild(_currentHintIndex).gameObject.SetActive(true);
            _currentHintIndex++;
        }
        else
        {
            _currentHints.GetChild(_currentHintIndex-1).gameObject.SetActive(false);
            Debug.LogWarning("Все подсказки показаны");
        }
    }

    private Transform GetCurrentHints()
    {
        if (LevelManager.currentMode != LevelMode.Level) return null;
        switch (LevelManager.currentLevel)
        {
            case 1:
                return tutorial1.transform;
            case 2:
                return tutorial2.transform;
            case 3:
                return tutorial3.transform;
            case 4:
                return tutorial4.transform;
            case 5:
                return tutorial5.transform;
            case 6:
                return tutorial6.transform;
            default:
                return null;
        }
    }
}