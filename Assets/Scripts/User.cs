using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class User
{
    public string nickname { get; set; }
    public Dictionary<Tuple<BloodType, RhesusFactor>, Statistics.StatisticsData> bloodGroupStatistics { get; set; }
    public int levels;

    public User(string nick)
    {
        nickname = nick;
        bloodGroupStatistics = Statistics.BloodGroupStatistics;
        levels = Statistics.levelsCompleted;
    }
}
