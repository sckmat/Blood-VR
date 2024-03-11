using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorial1;
    [SerializeField] private GameObject tutorial2;
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
            default:
                return null;
        }
    }
}
