using System.Collections.Generic;
using UnityEngine;

public static class BloodManager
{
    public static List<TestTube> testTubes { get; private set; } = new List<TestTube>();
    public static TestTube currentTestTube { get; private set; }
    
    public static void InitializeTestTubes()
    {
        for (int i = 0; i < testTubes.Count; i++)
        {
            if(LevelManager.instance.currentMode == LevelMode.Level && LevelManager.instance.levelBloodSample.Count == 1)
            {
                testTubes[i].bloodSample ??= CreateBloodSample(LevelManager.instance.levelBloodSample[0]);
            }            
            else if (LevelManager.instance.currentMode == LevelMode.Level && LevelManager.instance.levelBloodSample.Count > 1)
            {
                testTubes[i].bloodSample ??= CreateBloodSample(LevelManager.instance.levelBloodSample[i]);
            }
            else
                testTubes[i].bloodSample ??= CreateBloodSample();
            Debug.Log($"{testTubes[i].bloodSample.bloodType}{testTubes[i].bloodSample.rhesusFactor}");
        }
    }

    public static void ClearTestTubes()
    {
        testTubes.Clear();
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