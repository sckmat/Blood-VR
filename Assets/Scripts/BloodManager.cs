using System.Collections.Generic;
using UnityEngine;

public static class BloodManager
{
    private static List<TestTube> testTubes { get; set; } = new List<TestTube>();
    public static TestTube currentTestTube { get; private set; }
    
    public static void InitializeTestTubes()
    {
        foreach (var testTube in testTubes)
        {
            testTube.bloodSample = CreateRandomBloodSample();
            Debug.Log($"{testTube.bloodSample.bloodType}{testTube.bloodSample.rhesusFactor}");
        }
    }

    private static BloodSample CreateRandomBloodSample()
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

    public static void RemoveTestTube(TestTube testTube)
    {
        if (testTubes.Contains(testTube))
        {
            testTubes.Remove(testTube);
        }
    }
}
