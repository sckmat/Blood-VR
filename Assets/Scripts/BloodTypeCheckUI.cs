using TMPro;
using UnityEngine;

public class BloodTypeCheckUI : MonoBehaviour
{
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject loss;
    private BloodType _selectedBloodType;
    private RhesusFactor _selectedRhesusFactor;
    private static int _rightAnswers;

    public void SelectA() { _selectedBloodType = BloodType.A; }
    public void SelectB() { _selectedBloodType = BloodType.B; }
    public void SelectAB() { _selectedBloodType = BloodType.AB; }
    public void SelectO() { _selectedBloodType = BloodType.O; }
    public void SelectPositive() { _selectedRhesusFactor = RhesusFactor.Positive; }
    public void SelectNegative() { _selectedRhesusFactor = RhesusFactor.Negative; }

    public void CheckAnswer()
    {
        if(BloodManager.currentTestTube != null)
        {
            var bloodSample = BloodManager.currentTestTube.bloodSample;
            bool correctBloodType = _selectedBloodType == bloodSample.bloodType;
            bool correctRhesusFactor = _selectedRhesusFactor == bloodSample.rhesusFactor;

            if(correctBloodType && correctRhesusFactor)
            {
                resultText.text = "Ответ верный!";
                BloodManager.RemoveCompletedTestTube();
                TabletCircle.ResetCircleEvent.Invoke();
                if ((LevelManager.currentLevel == 5 && _rightAnswers < 2) || (LevelManager.currentLevel == 6 && _rightAnswers < 3))
                {
                    _rightAnswers++;
                }
                if (LevelManager.currentMode == LevelMode.FreeAccess || (LevelManager.currentLevel == 5 && _rightAnswers != 2) || (LevelManager.currentLevel == 6 && _rightAnswers != 3))
                {
                    return;
                }
                win.SetActive(true);
                loss.SetActive(false);
                gameObject.SetActive(false);
            }
            else
            {
                resultText.text = "Ответ неверный.";
                loss.SetActive(true);
            }
        }
        else
        {
            resultText.text = "Пробирка не найдена.";
        }
    }
}
