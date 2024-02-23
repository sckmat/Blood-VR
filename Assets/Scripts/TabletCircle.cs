using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TabletCircle : MonoBehaviour
{
    [SerializeField] private Material emptyMaterial;
    [SerializeField] private Material shapedElementsMaterial;
    [SerializeField] private Material serumMaterial;

    private Renderer _circleRenderer;

    private void Awake()
    {
        _circleRenderer = GetComponent<Renderer>(); 
        _circleRenderer.material = emptyMaterial;
    }

    public void Fill(PipetteState contents)
    {
        switch (contents)
        {
            case PipetteState.ShapedElements:
                FillWithShapedElements();
                break;
            case PipetteState.Serum:
                FillWithSerum();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(contents), contents, null);
        }
    }

    private void FillWithShapedElements()
    {
        _circleRenderer.material = shapedElementsMaterial;
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
