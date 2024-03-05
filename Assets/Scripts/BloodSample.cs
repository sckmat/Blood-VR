public class BloodSample
{
    public readonly BloodType bloodType;
    public readonly RhesusFactor rhesusFactor;
    
    public BloodSample(BloodType type, RhesusFactor factor)
    {
        bloodType = type;
        rhesusFactor = factor;
    }
}

public enum BloodType { A, B, AB, O }
public enum RhesusFactor { Positive, Negative }
