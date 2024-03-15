using System;

public class BloodSample
{
    public readonly BloodType bloodType;
    public readonly RhesusFactor rhesusFactor;
    
    public BloodSample(BloodType type, RhesusFactor factor)
    {
        bloodType = type;
        rhesusFactor = factor;
    }
    
    public override bool Equals(object obj)
    {
        return obj is BloodSample sample &&
               bloodType == sample.bloodType &&
               rhesusFactor == sample.rhesusFactor;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(bloodType, rhesusFactor);
    }
}

public enum BloodType { O, A, B, AB }
public enum RhesusFactor { Positive, Negative }