using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Statistics
{
    public int levelsCompleted { get; set; }
    [JsonConverter(typeof(BloodGroupStatisticsConverter))]
    public Dictionary<BloodSample, StatisticsData> BloodGroupStatistics = new Dictionary<BloodSample, StatisticsData>();

    public void UpdateStatistics(BloodSample bloodSample, bool successfulAnalysis)
    {
        var key = new BloodSample(bloodSample.bloodType, bloodSample.rhesusFactor);

        if (!BloodGroupStatistics.TryGetValue(key, out var groupStatistics))
        {
            groupStatistics = new StatisticsData();
        }

        groupStatistics.totalTests++;
        groupStatistics.successfulTests += successfulAnalysis ? 1 : 0;

        BloodGroupStatistics[key] = groupStatistics;
    }

    public float GetSuccessBloodGroupRate(BloodType bloodType, RhesusFactor rhesusFactor)
    {
        var key = new BloodSample(bloodType, rhesusFactor);
        if (BloodGroupStatistics.TryGetValue(key, out var statistics))
        {
            return CalculatePercentage(statistics);
        }
        return 0f;
    }

    public void UpdateLevelsCompleted()
    {
        if (levelsCompleted < LevelManager.currentLevel)
        {
            levelsCompleted = LevelManager.currentLevel;
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