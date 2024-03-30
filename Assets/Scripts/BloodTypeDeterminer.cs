using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodTypeDeterminer : MonoBehaviour
{
    private readonly ColycloneAgglutinationTable _aboTable = new();
    private readonly RhesusAgglutinationTable _rhesusTable = new();
    private readonly ErythrocyteAgglutinationTable _erythrocyteTable = new();

    public bool CheckAgglutination(BloodSample bloodSample, Colyclone colyclone = Colyclone.None, Erythrocyte erythrocyte = Erythrocyte.None)
    {
        var colycloneAgglutination = _aboTable.CheckAgglutination(bloodSample.bloodType, colyclone);
        var rhesusAgglutination = _rhesusTable.CheckAgglutination(bloodSample.rhesusFactor, colyclone);
        var erythrocyteAgglutination = _erythrocyteTable.CheckAgglutination(bloodSample.bloodType, erythrocyte);
        
        return colycloneAgglutination || rhesusAgglutination || erythrocyteAgglutination;
    }
}

public class ColycloneAgglutinationTable
{
    private readonly Dictionary<BloodType, Dictionary<Colyclone, bool>> _aboAgglutinationRules = new()
    {
        { BloodType.A, new Dictionary<Colyclone, bool>() { { Colyclone.AntiA, true }, { Colyclone.AntiB, false } } },
        { BloodType.B, new Dictionary<Colyclone, bool>() { { Colyclone.AntiA, false }, { Colyclone.AntiB, true } } },
        { BloodType.AB, new Dictionary<Colyclone, bool>() { { Colyclone.AntiA, true }, { Colyclone.AntiB, true } } },
        { BloodType.O, new Dictionary<Colyclone, bool>() { { Colyclone.AntiA, false }, { Colyclone.AntiB, false } } },
    };

    public bool CheckAgglutination(BloodType bloodType, Colyclone colyclone)
    {
        if (!_aboAgglutinationRules.TryGetValue(bloodType, out var colycloneRules)) return false;
        return colycloneRules.TryGetValue(colyclone, out var doesAgglutinate) && doesAgglutinate;
    }
}

public class ErythrocyteAgglutinationTable
{
    private readonly Dictionary<BloodType, Dictionary<Erythrocyte, bool>> _erythrocyteAgglutinationRules = new()
    {
        { BloodType.A, new Dictionary<Erythrocyte, bool>() { { Erythrocyte.ErythrocyteO , false}, { Erythrocyte.ErythrocyteB, true }, { Erythrocyte.ErythrocyteA, false } } },
        { BloodType.B, new Dictionary<Erythrocyte, bool>() { { Erythrocyte.ErythrocyteO , false}, { Erythrocyte.ErythrocyteB, false }, { Erythrocyte.ErythrocyteA, true } } },
        { BloodType.AB, new Dictionary<Erythrocyte, bool>() { { Erythrocyte.ErythrocyteO , false}, { Erythrocyte.ErythrocyteB, false }, { Erythrocyte.ErythrocyteA, false } } },
        { BloodType.O, new Dictionary<Erythrocyte, bool>() { { Erythrocyte.ErythrocyteO , false}, { Erythrocyte.ErythrocyteB, true }, { Erythrocyte.ErythrocyteA, true } } },
    };
    
    public bool CheckAgglutination(BloodType bloodType, Erythrocyte erythrocyte)
    {
        if (!_erythrocyteAgglutinationRules.TryGetValue(bloodType, out var erythrocyteRules)) return false; 
        return erythrocyteRules.TryGetValue(erythrocyte, out var doesAgglutinate) && doesAgglutinate;
    }
}


public class RhesusAgglutinationTable
{
    private readonly Dictionary<RhesusFactor, bool> _rhesusAgglutinationRules = new()
    {
        { RhesusFactor.Positive, true }, 
        { RhesusFactor.Negative, false },
    };
    
    public bool CheckAgglutination(RhesusFactor rhesus, Colyclone colyclone)
    {
        return colyclone == Colyclone.AntiD && _rhesusAgglutinationRules[rhesus];
    }
}