using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public static class Statistics
{
    public struct StatisticsData
    {
        public int totalTests { get; set; }
        public int successfulTests { get; set; }
    }
    
    public static int levelsCompleted { get; set; }
    public static readonly Dictionary<Tuple<BloodType, RhesusFactor>, StatisticsData> BloodGroupStatistics = new Dictionary<Tuple<BloodType, RhesusFactor>, StatisticsData>();

    public static void UpdateStatistics(BloodSample bloodSample, bool successfulAnalysis)
    {
        var key = Tuple.Create(bloodSample.bloodType, bloodSample.rhesusFactor);

        if (!BloodGroupStatistics.TryGetValue(key, out var groupStatistics))
        {
            groupStatistics = new StatisticsData();
        }

        groupStatistics.totalTests++;
        groupStatistics.successfulTests += successfulAnalysis ? 1 : 0;

        BloodGroupStatistics[key] = groupStatistics;
    }

    public static float GetSuccessBloodGroupRate(BloodType bloodType, RhesusFactor rhesusFactor)
    {
        var key = Tuple.Create(bloodType, rhesusFactor);
        if (BloodGroupStatistics.TryGetValue(key, out var statistics))
        {
            return CalculatePercentage(statistics);
        }
        return 0f;
    }

    public static void UpdateLevelsCompleted()
    {
        levelsCompleted ++;
    }
    
    private static float CalculatePercentage(StatisticsData statistics)
    {
        return statistics.totalTests == 0 ? 0f : (float)statistics.successfulTests / statistics.totalTests * 100f;
    }
}