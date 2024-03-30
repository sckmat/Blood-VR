using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Statistics
{
    public int levelsCompleted { get; set; }
    
    [JsonConverter(typeof(BloodGroupStatisticsConverter))]
    public readonly Dictionary<BloodSample, StatisticsData> bloodGroupStatistics;

    public Statistics(Dictionary<BloodSample, StatisticsData> bloodGroupStatistics)
    {
        this.bloodGroupStatistics = bloodGroupStatistics;
    }

    public void UpdateStatistics(BloodSample bloodSample, bool successfulAnalysis)
    {
        var key = new BloodSample(bloodSample.bloodType, bloodSample.rhesusFactor);

        if (!bloodGroupStatistics.TryGetValue(key, out var groupStatistics))
        {
            groupStatistics = new StatisticsData();
        }

        groupStatistics.totalTests++;
        groupStatistics.successfulTests += successfulAnalysis ? 1 : 0;

        bloodGroupStatistics[key] = groupStatistics;
    }

    public float GetSuccessBloodGroupRate(BloodType bloodType, RhesusFactor rhesusFactor)
    {
        var key = new BloodSample(bloodType, rhesusFactor);
        if (bloodGroupStatistics.TryGetValue(key, out var statistics))
        {
            return CalculatePercentage(statistics);
        }
        return 0f;
    }

    public void UpdateLevelsCompleted()
    {
        if (levelsCompleted < LevelManager.instance.currentLevel)
        {
            levelsCompleted = LevelManager.instance.currentLevel;
        }
    }
    
    private static float CalculatePercentage(StatisticsData statistics)
    {
        return statistics.totalTests == 0 ? 0f : (float)statistics.successfulTests / statistics.totalTests * 100f;
    }
}

public struct StatisticsData
{
    public int totalTests { get; set; }
    public int successfulTests { get; set; }
}