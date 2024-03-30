using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TabletCircleVisual))]
[RequireComponent(typeof(BloodTypeDeterminer))]

public class TabletCircle : MonoBehaviour
{
    public CircleState currentState = CircleState.Empty;
    private Colyclone _colyclone = Colyclone.None;
    private Erythrocyte _erythrocyte = Erythrocyte.None;
    public static readonly UnityEvent ResetCircleEvent = new ();
    private BloodSample _currentBloodSample;
    private TabletCircleVisual _visual;
    private BloodTypeDeterminer _bloodTypeDeterminer;


    private void Awake()
    {
        _visual = GetComponent<TabletCircleVisual>();
        _visual.UpdateMaterial(CircleState.Empty);
        _bloodTypeDeterminer = GetComponent<BloodTypeDeterminer>();
        ResetCircleEvent.AddListener(ResetCircle);
    }

    public void AddReagent(Reagent reagent)
    {
        switch(reagent.reagentType)
        {
            case ReagentType.Colyclone:
                _colyclone = reagent.colyclone;
                if (currentState == CircleState.FormedElements)
                {
                    SetState(CircleState.FormedElementsReagentMix);
                }
                else
                {
                    SetState(CircleState.Colyclone, reagent.colyclone);
                }                
                break;
            case ReagentType.Erythrocyte:
                _erythrocyte = reagent.erythrocyte;
                SetState(currentState == CircleState.Serum ? CircleState.SerumReagentMix : CircleState.Erythrocyte);
                break;
        }
    }
    
    public void AddFormedElements(BloodSample bloodSample)
    {
        if (currentState == CircleState.Colyclone) 
        {
            _currentBloodSample = bloodSample;
            SetState(CircleState.FormedElementsReagentMix);
        }
        else if (currentState == CircleState.Empty)
        {
            _currentBloodSample = bloodSample;
            SetState(CircleState.FormedElements);
        }
    }

    public void AddSerum(BloodSample bloodSample)
    {
        if (currentState == CircleState.Erythrocyte)
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
        currentState = newState;
        _visual.UpdateMaterial(currentState, colyclone);
    }
    
    public void CheckAgglutination()
    {
        if (_erythrocyte is Erythrocyte.None && _colyclone  is Colyclone.None || _currentBloodSample == null) return; 
        Debug.Log($"{_currentBloodSample.bloodType} {_currentBloodSample.rhesusFactor}");
        var agglutination = _bloodTypeDeterminer.CheckAgglutination(_currentBloodSample, _colyclone, _erythrocyte);
        SetState(agglutination ? CircleState.Agglutination : CircleState.NoAgglutination);
    }

    private void ResetCircle()
    {
        _colyclone = Colyclone.None;
        _erythrocyte = Erythrocyte.None;
        currentState = CircleState.Empty;
        _visual.UpdateMaterial(currentState);
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