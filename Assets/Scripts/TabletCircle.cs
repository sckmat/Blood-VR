using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TabletCircle : MonoBehaviour
{
    [SerializeField] private Material emptyMaterial;
    [SerializeField] private Material formedElementsMaterial;
    [SerializeField] private Material serumMaterial;
    [SerializeField] private Material colycloneMaterial;
    [SerializeField] private Material agglutinationMaterial;
    [SerializeField] private Material noAgglutinationMaterial;

    private bool hasSerum = false;
    private bool hasFormedElements = false;
    private bool _hasAntiA = false;
    private bool _hasAntiB = false;
    private bool _hasAntiD = false;
    private bool _hasErythrocyteA = false;
    private bool _hasErythrocyteB = false;
    private bool _hasErythrocyteO = false;

    public bool agglutinatedProcess = false;

    private Renderer _circleRenderer;

    private void Awake()
    {
        _circleRenderer = GetComponent<Renderer>(); 
        _circleRenderer.material = emptyMaterial;
    }

    public void AddFromTestTube(PipetteState contents)
    {
        switch (contents)
        {
            case PipetteState.FormedElements:
                AddFormedElements();
                break;
            case PipetteState.Serum:
                AddSerum();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(contents), contents, null);
        }
    }

    public void AddFromReagent(ReagentType contents)
    {
        if ((hasFormedElements && !_hasAntiA && !_hasAntiB && !_hasAntiD) || (hasSerum && !_hasErythrocyteO && !_hasErythrocyteA && !_hasErythrocyteB))
        {
            if (contents == ReagentType.AntiA)
            {
                _hasAntiA = true;
            }

            if (contents == ReagentType.AntiB)
            {
                _hasAntiB = true;
            }
            
            if (contents == ReagentType.AntiD)
            {
                _hasAntiD = true;
            }
            
            if (contents == ReagentType.ErythrocyteO)
            {
                _hasErythrocyteO = true;
            }
            
            if (contents == ReagentType.ErythrocyteA)
            {
                _hasErythrocyteA = true;
            }
            
            if (contents == ReagentType.ErythrocyteB)
            {
                _hasErythrocyteB = true;
            }
        }
    }
    
    private void AddFormedElements()
    {
        if (!hasFormedElements && !hasSerum)
        {
            hasFormedElements = true;
            _circleRenderer.material = formedElementsMaterial;
        }
    }

    private void AddSerum()
    {
        if (!hasSerum && !hasFormedElements) 
        {
            hasSerum = true;
            _circleRenderer.material = serumMaterial;
        }
    }

    public void CheckAgglutination()
    {
        if (!_hasAntiA && !_hasAntiB && !_hasAntiD && !_hasErythrocyteA && !_hasErythrocyteO && !_hasErythrocyteB) return; 

        bool agglutination = false;
        if (TestTube.bloodSample != null)
        {
            BloodType bloodType = TestTube.bloodSample.bloodType;
            RhesusFactor rhesusFactor = TestTube.bloodSample.rhesusFactor;
            if (hasFormedElements && 
                ((_hasAntiA && bloodType == BloodType.A) || (_hasAntiB && bloodType == BloodType.B) ||
                ((_hasAntiA || _hasAntiB) && bloodType == BloodType.AB) ||
                (!_hasAntiA && !_hasAntiB && bloodType == BloodType.O) ||
                _hasAntiD && rhesusFactor == RhesusFactor.Positive))
            {
                agglutination = true;
            }
            
            if (hasSerum && 
                ((_hasErythrocyteB && bloodType == BloodType.A) ||
                 (_hasErythrocyteA && bloodType == BloodType.B) ||
                 ((_hasErythrocyteA || _hasErythrocyteB) && bloodType == BloodType.O)))
            {
                agglutination = true;
            }
        }

        _circleRenderer.material = agglutination ? agglutinationMaterial : noAgglutinationMaterial;
        agglutinatedProcess = true;
    }

}
