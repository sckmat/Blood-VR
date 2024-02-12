using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSample : MonoBehaviour
{
    public enum BloodType { A, B, AB, O }
    public enum RhesusFactor { Positive, Negative }

    public BloodType bloodType;
    public RhesusFactor rhesusFactor;
    
    public BloodSample(BloodType type, RhesusFactor factor)
    {
        bloodType = type;
        rhesusFactor = factor;
    }
}
