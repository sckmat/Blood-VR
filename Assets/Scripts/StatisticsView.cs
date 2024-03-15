using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsView : MonoBehaviour
{
    [SerializeField] private TMP_Text result;
    [SerializeField] private Button getResult;

    private void Awake()
    {
        getResult.onClick.AddListener(Result);
    }

    private void Result()
    {
        result.text = UserManager.currentUser.statistics.GetSuccessBloodGroupRate(BloodType.A, RhesusFactor.Negative).ToString(CultureInfo.CurrentCulture);
    }
}
