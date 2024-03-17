using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsView : MonoBehaviour
{
    [SerializeField] private GameObject statisticsRowPrefab;
    [SerializeField] private Transform contentPanel;

    private void Awake()
    {
        DisplayStatistics(UserManager.currentUser.statistics.bloodGroupStatistics);
        Debug.LogWarning(UserManager.currentUser.statistics.bloodGroupStatistics);

    }

    private void DisplayStatistics(Dictionary<BloodSample, StatisticsData> bloodGroupStatistics)
    {
        Debug.LogWarning(bloodGroupStatistics.Count);

        foreach (var entry in bloodGroupStatistics)
        {
            var rowInstance = Instantiate(statisticsRowPrefab, contentPanel);
            var textInstance = rowInstance.transform.GetChild(0);
            textInstance.GetChild(0).GetComponent<TMP_Text>().text = entry.Key.bloodType.ToString();
            textInstance.GetChild(1).GetComponent<TMP_Text>().text = entry.Key.rhesusFactor == RhesusFactor.Positive ? "Положительный" : "Отрицательный";
            textInstance.GetChild(2).GetComponent<TMP_Text>().text = CalculatePercentage(entry.Value) + "%";
        }
    }

    private string CalculatePercentage(StatisticsData statistics)
    {
        if (statistics.totalTests == 0) return "N/A";
        int percentage = (int)((float)statistics.successfulTests / statistics.totalTests * 100);
        return percentage.ToString();
    }
}
