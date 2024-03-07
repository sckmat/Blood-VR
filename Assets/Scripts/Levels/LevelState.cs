using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelState
{
    public abstract void Initialize();
    public abstract BloodSample GetBloodSample();
    public abstract void LoadObjects();
    public abstract void SetupEnvironment();
    public abstract void Cleanup();
}
