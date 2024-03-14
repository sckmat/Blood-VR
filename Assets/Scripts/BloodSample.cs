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

public enum BloodType { O, A, B, AB }
public enum RhesusFactor { Positive, Negative }