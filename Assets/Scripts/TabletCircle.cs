using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

public class TabletCircle : MonoBehaviour
{
    private Dictionary<CircleState, List<Material>> _materials;
    public CircleState currentState = CircleState.Empty;
    private  HashSet<Colyclone> _colyclones = new HashSet<Colyclone>();
    private  HashSet<Erythrocyte> _erythrocytes = new HashSet<Erythrocyte>();
    
    private Renderer _circleRenderer;
    public static readonly UnityEvent ResetCircleEvent = new UnityEvent(); 

    private void Awake()
    {
        LoadMaterials();
        _circleRenderer = GetComponent<Renderer>(); 
        _circleRenderer.material = _materials[currentState][0];
        ResetCircleEvent.AddListener(ResetCircle);
    }

    private void LoadMaterials()
    {
        _materials = new Dictionary<CircleState, List<Material>>
        {
            { CircleState.Empty, new List<Material>(Resources.LoadAll<Material>("Materials/Empty")) },
            { CircleState.Serum, new List<Material>(Resources.LoadAll<Material>("Materials/Serum")) },
            { CircleState.FormedElements, new List<Material>(Resources.LoadAll<Material>("Materials/FormedElements")) },
            { CircleState.Colyclone, new List<Material>(Resources.LoadAll<Material>("Materials/Colyclone")) },
            { CircleState.Erythrocyte, new List<Material>(Resources.LoadAll<Material>("Materials/Erythrocyte")) },
            { CircleState.Agglutination,  new List<Material>(Resources.LoadAll<Material>("Materials/Agglutination"))},
            { CircleState.NoAgglutination, new List<Material>(Resources.LoadAll<Material>("Materials/NoAgglutination"))}
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
        if (currentState is CircleState.Empty or CircleState.Colyclone)
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
        if (currentState == CircleState.Colyclone && _colyclones.Count > 0)
        {
            Material selectedMaterial = null;
        
            if (_colyclones.Contains(Colyclone.AntiA))
                selectedMaterial = _materials[CircleState.Colyclone].Find(m => m.name.Contains("AntiA"));
            else if (_colyclones.Contains(Colyclone.AntiB))
                selectedMaterial = _materials[CircleState.Colyclone].Find(m => m.name.Contains("AntiB"));
            else if (_colyclones.Contains(Colyclone.AntiD))
                selectedMaterial = _materials[CircleState.Colyclone].Find(m => m.name.Contains("AntiD"));

            if (selectedMaterial != null)
            {
                _circleRenderer.material = selectedMaterial;
                return;
            }
        }

        SetRandomMaterial();
    }
    
    private void SetRandomMaterial()
    {
        if (_materials.TryGetValue(currentState, out var materialsList))
        {
            if (materialsList.Count > 0)
            {
                var randomIndex = new Random().Next(materialsList.Count);
                _circleRenderer.material = materialsList[randomIndex];
            }
            else
            {
                Debug.LogWarning("Нет доступных материалов для текущего состояния.");
            }
        }
        else
        {
            Debug.LogWarning($"Материалы для состояния {currentState} не найдены.");
        }
    }
    
    public void CheckAgglutination()
    {
        var bloodSample = BloodManager.currentTestTube?.bloodSample;
        Debug.LogWarning("Aggl " + bloodSample);
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

    private void ResetCircle()
    {
        _colyclones = new HashSet<Colyclone>();
        _erythrocytes = new HashSet<Erythrocyte>();
        currentState = CircleState.Empty;
        UpdateMaterial();
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
