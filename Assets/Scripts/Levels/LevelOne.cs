using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LevelOne  : LevelState
{
    public override void Initialize()
    {
        
    }

    public override BloodSample GetBloodSample()
    {
        return new BloodSample(BloodType.A, RhesusFactor.Positive);
    }

    public override void LoadObjects()
    {
        throw new System.NotImplementedException();
    }

    public override void SetupEnvironment()
    {
        throw new System.NotImplementedException();
    }

    public override void Cleanup()
    {
        throw new System.NotImplementedException();
    }
}
