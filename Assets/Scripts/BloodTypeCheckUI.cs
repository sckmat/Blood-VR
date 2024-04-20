using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BloodTypeCheckUI : MonoBehaviour
{
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject loss;
    [SerializeField] private List<Button> bloodTypeButtons;
    [SerializeField] private List<Button> rhButtons;

    private BloodType _selectedBloodType;
    private RhesusFactor _selectedRhesusFactor;
    private static int _rightAnswers;

    private void Awake()
    {
        for (int i = 0; i < bloodTypeButtons.Count; i++)
        {
            BloodType bloodType = (BloodType)i;
            bloodTypeButtons[i].onClick.AddListener(() => SelectBloodType(bloodType));
        }

        for (int i = 0; i < rhButtons.Count; i++)
        {
            RhesusFactor rhFactor = (RhesusFactor)i;
            rhButtons[i].onClick.AddListener(() => SelectRhesusFactor(rhFactor));
        }
    }

    private void SelectBloodType(BloodType bloodType)
    {
        _selectedBloodType = bloodType;
        DeactivateButtons(bloodTypeButtons, bloodType);
    }

    private void SelectRhesusFactor(RhesusFactor rhFactor)
    {
        _selectedRhesusFactor = rhFactor;
        DeactivateButtons(rhButtons, rhFactor);
    }

    private void DeactivateButtons<T>(List<Button> buttons, T selection)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = !Equals(selection, (T)(object)i);
        }
    }

    public void CheckAnswer()
    {
        if (BloodManager.currentTestTube == null)
        {
            resultText.text = "Пробирка не найдена.";
            return;
        }

        var bloodSample = BloodManager.currentTestTube.bloodSample;
        bool correctBloodType = _selectedBloodType == bloodSample.bloodType;
        bool correctRhesusFactor = _selectedRhesusFactor == bloodSample.rhesusFactor;

        if (correctBloodType && correctRhesusFactor)
        {
            HandleCorrectAnswer();
        }
        else
        {
            resultText.text = "Ответ неверный.";
            UserManager.currentUser.statistics.UpdateStatistics(BloodManager.currentTestTube.bloodSample, false);
            if(LevelManager.instance.currentMode == LevelMode.FreeAccess) return;
            loss.SetActive(true);
        }
        SaveManager.instance.SaveData(UserManager.currentUser);
    }

    private void HandleCorrectAnswer()
    {
        resultText.text = "Ответ верный!";
        loss.SetActive(false);
        UserManager.currentUser.statistics.UpdateStatistics(BloodManager.currentTestTube.bloodSample, true);
        UserManager.currentUser.statistics.UpdateLevelsCompleted();

        BloodManager.RemoveCompletedTestTube();
        TabletCircle.ResetCircleEvent.Invoke();
        UpdateRightAnswers();
        if (!ShouldShowWin())
        {
            ShowWin();
            _rightAnswers = 0;
        }
    }

    private void UpdateRightAnswers()
    {
        if ((LevelManager.instance.currentLevel == 5 && _rightAnswers < 2) || (LevelManager.instance.currentLevel == 6 && _rightAnswers < 3))
        {
            _rightAnswers++;
            Debug.Log(_rightAnswers);
        }
    }

    private bool ShouldShowWin()
    {
        return LevelManager.instance.currentMode == LevelMode.FreeAccess || 
               (LevelManager.instance.currentLevel == 5 && _rightAnswers != 2) || 
               (LevelManager.instance.currentLevel == 6 && _rightAnswers != 3);
    }

    private void ShowWin()
    {
        win.SetActive(true);
        loss.SetActive(false);
        gameObject.SetActive(false);
    }
}