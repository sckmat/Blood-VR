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
        if (hasFormedElements && !_hasAntiA && !_hasAntiB)
        {
            if (contents == ReagentType.AntiA)
            {
                _hasAntiA = true;
            }

            if (contents == ReagentType.AntiB)
            {
                _hasAntiB = true;
            }
            CheckAgglutination();
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

    private void CheckAgglutination()
    {
        if (!_hasAntiA && !_hasAntiB) return; // Если цоликлоны не добавлены, нет смысла проверять

        bool agglutinationHappened = false;
        if (TestTube.bloodSample != null)
        {
            BloodType bloodType = TestTube.bloodSample.bloodType;
            if ((_hasAntiA && bloodType == BloodType.A) || (_hasAntiB && bloodType == BloodType.B) ||
                (_hasAntiA && _hasAntiB && bloodType == BloodType.AB) ||
                (!_hasAntiA && !_hasAntiB && bloodType == BloodType.O))
            {
                agglutinationHappened = true;
            }
        }

        _circleRenderer.material = agglutinationHappened ? agglutinationMaterial : noAgglutinationMaterial;
    }

}
