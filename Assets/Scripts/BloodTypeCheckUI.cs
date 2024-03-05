using TMPro;
using UnityEngine;

public class BloodTypeCheckUI : MonoBehaviour
{
    public TMP_Text resultText;

    private BloodType _selectedBloodType;
    private RhesusFactor _selectedRhesusFactor;

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
            }
            else
            {
                resultText.text = "Ответ неверный, попробуйте еще раз.";
            }
        }
        else
        {
            resultText.text = "Образец крови не найден.";
        }
    }
}