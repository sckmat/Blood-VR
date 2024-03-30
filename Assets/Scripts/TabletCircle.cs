using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TabletCircleVisuals))]
public class TabletCircle : MonoBehaviour
{
    public CircleState currentState = CircleState.Empty;
    private HashSet<Colyclone> _colyclones = new HashSet<Colyclone>();
    private HashSet<Erythrocyte> _erythrocytes = new HashSet<Erythrocyte>();
    public static readonly UnityEvent ResetCircleEvent = new UnityEvent();
    private BloodSample _currentBloodSample;
    private TabletCircleVisuals _visuals;

    private void Awake()
    {
        _visuals = GetComponent<TabletCircleVisuals>();
        _visuals.UpdateMaterial(CircleState.Empty);
        ResetCircleEvent.AddListener(ResetCircle);
    }

    public void AddReagent(Reagent reagent)
    {
        switch(reagent.reagentType)
        {
            case ReagentType.Colyclone:
                _colyclones.Add(reagent.colyclone);
                if (currentState == CircleState.FormedElements)
                {
                    SetState(CircleState.FormedElementsReagentMix);
                }
                else
                {
                    SetState(CircleState.Colyclone, reagent.colyclone);
                }                break;
            case ReagentType.Erythrocyte:
                _erythrocytes.Add(reagent.erythrocyte);
                SetState(currentState == CircleState.Serum ? CircleState.SerumReagentMix : CircleState.Erythrocyte);
                break;
        }
    }
    
    public void AddFormedElements(BloodSample bloodSample)
    {
        if (currentState == CircleState.Colyclone) 
        {
            SetState(CircleState.FormedElementsReagentMix);
            _currentBloodSample = bloodSample;
        }
        else if (currentState == CircleState.Empty)
        {
            _currentBloodSample = bloodSample;
            SetState(CircleState.FormedElements);
        }
    }

    public void AddSerum(BloodSample bloodSample)
    {
        if (currentState == CircleState.Colyclone)
        {
            _currentBloodSample = bloodSample;
            SetState(CircleState.SerumReagentMix);
        }
        else if (currentState == CircleState.Empty)
        {
            _currentBloodSample = bloodSample;
            SetState(CircleState.Serum);
        }
    }
    
    private void SetState(CircleState newState, Colyclone? colyclone = null)
    {
        if (currentState != CircleState.Empty && newState is CircleState.Colyclone or CircleState.Erythrocyte)
        {
            return;
        }
        currentState = newState;
        _visuals.UpdateMaterial(currentState, colyclone);
    }
    
    public void CheckAgglutination()
    {
        if (_erythrocytes.Count == 0 && _colyclones.Count == 0 || _currentBloodSample == null) return; 
        var bloodType = _currentBloodSample.bloodType;
        var rhesusFactor = _currentBloodSample.rhesusFactor;
        Debug.Log($"CheckAgglutination {bloodType} {rhesusFactor}");

        var agglutination = currentState switch
        {
            CircleState.FormedElementsReagentMix => (_colyclones.Contains(Colyclone.AntiA) && bloodType == BloodType.A) ||
                                          (_colyclones.Contains(Colyclone.AntiB) && bloodType == BloodType.B) ||
                                          ((_colyclones.Contains(Colyclone.AntiA) || _colyclones.Contains(Colyclone.AntiB)) 
                                          && bloodType == BloodType.AB) ||
                                          (_colyclones.Contains(Colyclone.AntiD) && rhesusFactor == RhesusFactor.Positive),
            CircleState.SerumReagentMix => (_erythrocytes.Contains(Erythrocyte.ErythrocyteB) && bloodType == BloodType.A) ||
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
        _visuals.UpdateMaterial(currentState);
    }
}

public enum CircleState
{
    Empty,
    FormedElements,
    Serum,
    Colyclone,
    Erythrocyte,
    SerumReagentMix,
    FormedElementsReagentMix,
    Agglutination,
    NoAgglutination
}