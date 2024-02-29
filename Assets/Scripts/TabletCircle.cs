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


    private Renderer _circleRenderer;

    private void Awake()
    {
        _circleRenderer = GetComponent<Renderer>(); 
        _circleRenderer.material = emptyMaterial;
    }

    public void FillFromTestTube(PipetteState contents)
    {
        switch (contents)
        {
            case PipetteState.FormedElements:
                FillWithFormedElements();
                break;
            case PipetteState.Serum:
                FillWithSerum();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(contents), contents, null);
        }
    }
    
    public void FillFromReagent(ReagentType contents)
    {
        switch (contents)
        {
            case ReagentType.AntiA:
                FillWithColyclone();
                break;
            case ReagentType.AntiB:
                FillWithColyclone();
                break;
            case ReagentType.AntiD:
                FillWithColyclone();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(contents), contents, null);
        }
    }

    private void FillWithColyclone()
    {
        _circleRenderer.material = colycloneMaterial;
    }
    private void FillWithFormedElements()
    {
        _circleRenderer.material = formedElementsMaterial;
    }

    private void FillWithSerum()
    {
        _circleRenderer.material = serumMaterial;
    }

    private void Clear()
    {
        _circleRenderer.material = emptyMaterial;
    }
}
