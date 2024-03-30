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

    public void UpdateLevelsCompleted()
    {
        if (levelsCompleted < LevelManager.instance.currentLevel)
        {
            levelsCompleted = LevelManager.instance.currentLevel;
        }
    }
}

public struct StatisticsData
{
    public int totalTests { get; set; }
    public int successfulTests { get; set; }
}