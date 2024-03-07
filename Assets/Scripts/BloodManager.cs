using System.Collections.Generic;
using UnityEngine;

public static class BloodManager
{
    public static List<TestTube> testTubes { get; private set; } = new List<TestTube>();
    public static TestTube currentTestTube { get; private set; }
    
    public static void InitializeTestTubes()
    {
        foreach (var testTube in testTubes)
        {
            if(LevelManager.currentMode == LevelMode.Level) 
                testTube.bloodSample ??= CreateBloodSample(LevelManager.levelState.GetBloodSample());
            else
                testTube.bloodSample ??= CreateBloodSample();
            Debug.Log($"{testTube.bloodSample.bloodType}{testTube.bloodSample.rhesusFactor}");
        }
    }

    private static BloodSample CreateBloodSample(BloodSample bloodSample)
    {
        return new BloodSample(bloodSample.bloodType, bloodSample.rhesusFactor);
    }
    
    private static BloodSample CreateBloodSample()
    {
        BloodType randomBloodType = (BloodType)Random.Range(0, 4);
        RhesusFactor randomRhesusFactor = (RhesusFactor)Random.Range(0, 2);
        return new BloodSample(randomBloodType, randomRhesusFactor);
    }

    public static void SetCurrentTestTube(TestTube testTube)
    {
        currentTestTube = testTube;
    }
    
    public static void RemoveCurrentTestTube()
    {
        currentTestTube = null;
    }

    public static void AddTestTube(TestTube testTube)
    {
        if (!testTubes.Contains(testTube))
        {
            testTubes.Add(testTube);
        }
    }

    public static void RemoveCompletedTestTube()
    {
        if (testTubes.Contains(currentTestTube))
        {
            testTubes.Remove(currentTestTube);
            currentTestTube.DestroyTestTube();
            Debug.Log(currentTestTube);
        }
    }
}