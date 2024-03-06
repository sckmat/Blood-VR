using System;
using System.Collections.Generic;
using UnityEngine;

public class TabletCircle : MonoBehaviour
{
    private Dictionary<CircleState, Material> _materials;
    public CircleState currentState = CircleState.Empty;
    private readonly HashSet<Colyclone> _colyclones = new HashSet<Colyclone>();
    private readonly HashSet<Erythrocyte> _erythrocytes = new HashSet<Erythrocyte>();
    
    private Renderer _circleRenderer;

    private void Awake()
    {
        LoadMaterials();
        _circleRenderer = GetComponent<Renderer>(); 
        _circleRenderer.material = _materials[currentState];
    }

    private void LoadMaterials()
    {
        _materials = new Dictionary<CircleState, Material>
        {
            { CircleState.Empty, Resources.Load<Material>("Materials/Empty") },
            { CircleState.Serum, Resources.Load<Material>("Materials/Serum") },
            { CircleState.FormedElements, Resources.Load<Material>("Materials/FormedElements") },
            { CircleState.Colyclone, Resources.Load<Material>("Materials/Colyclone") },
            { CircleState.Erythrocyte, Resources.Load<Material>("Materials/Erythrocyte") },
            { CircleState.Agglutination,  Resources.Load<Material>("Materials/Agglutination")},
            { CircleState.NoAgglutination, Resources.Load<Material>("Materials/NoAgglutination")}
        };
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

    public void AddFromReagent(Reagent reagent)
    {
        switch(reagent.reagentType)
        {
            case ReagentType.Colyclone:
                _colyclones.Add(reagent.colyclone);
                SetState(CircleState.Colyclone);
                break;
            case ReagentType.Erythrocyte:
                _erythrocytes.Add(reagent.erythrocyte);
                SetState(CircleState.Erythrocyte);
                break;
        }
    }
    
    private void AddFormedElements()
    {
        if (currentState == CircleState.Empty || currentState == CircleState.Colyclone)
        {
            SetState(CircleState.FormedElements);
        }
    }

    private void AddSerum()
    {
        if (currentState == CircleState.Empty || currentState == CircleState.Erythrocyte) 
        {
            SetState(CircleState.Serum);
        }
    }
    
    private void SetState(CircleState newState)
    {
        if (currentState != CircleState.Empty && newState is CircleState.Colyclone or CircleState.Erythrocyte)
        {
            return;
        }
        currentState = newState;
        UpdateMaterial();
    }

    private void UpdateMaterial()
    {
        if (_materials.TryGetValue(currentState, out Material newMaterial))
        {
            _circleRenderer.material = newMaterial;
        }
    }
    
    public void CheckAgglutination()
    {
        var bloodSample = BloodManager.currentTestTube?.bloodSample;
        if (_erythrocytes.Count == 0 && _colyclones.Count == 0 || bloodSample == null) return; 
        var bloodType = bloodSample.bloodType;
        var rhesusFactor = bloodSample.rhesusFactor;
        Debug.Log($"CheckAgglutination {bloodType} {rhesusFactor}");

        var agglutination = currentState switch
        {
            CircleState.FormedElements => (_colyclones.Contains(Colyclone.AntiA) && bloodType == BloodType.A) ||
                                          (_colyclones.Contains(Colyclone.AntiB) && bloodType == BloodType.B) ||
                                          ((_colyclones.Contains(Colyclone.AntiA) || _colyclones.Contains(Colyclone.AntiB)) 
                                          && bloodType == BloodType.AB) ||
                                          (_colyclones.Contains(Colyclone.AntiD) && rhesusFactor == RhesusFactor.Positive),
            CircleState.Serum => (_erythrocytes.Contains(Erythrocyte.ErythrocyteB) && bloodType == BloodType.A) ||
                                 (_erythrocytes.Contains(Erythrocyte.ErythrocyteA) && bloodType == BloodType.B) ||
                                 ((_erythrocytes.Contains(Erythrocyte.ErythrocyteA) ||
                                   _erythrocytes.Contains(Erythrocyte.ErythrocyteB)) && bloodType == BloodType.O),
            _ => false
        };
        SetState(agglutination ? CircleState.Agglutination : CircleState.NoAgglutination);
    }

    public enum CircleState
    {
        Empty,
        FormedElements,
        Serum,
        Colyclone,
        Erythrocyte,
        Agglutination,
        NoAgglutination
    }
}
