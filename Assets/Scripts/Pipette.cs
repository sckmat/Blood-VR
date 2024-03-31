using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PipetteVisual))]
public class Pipette : MonoBehaviour
{
    private PipetteState _currentState = PipetteState.Empty;
    private TabletCircle _targetedCircle;
    private int _usesLeft = 3;
    private Reagent _currentReagent;
    private BloodSample _currentBloodSample;
    private PipetteVisual _pipetteVisual;

    private void Awake()
    {
        _pipetteVisual = GetComponent<PipetteVisual>();
    }

    public void Activate()
    {
        if (_targetedCircle == null || _currentState == PipetteState.Empty) return;

        IAdditive additive = null;
        var state = _targetedCircle.currentState;
        switch (_currentState)
        {
            case PipetteState.FormedElements:
                if (state == CircleState.Empty || state == CircleState.Colyclone) 
                    additive = new FormedElementsAdditive(_currentBloodSample);
                else
                {
                   AudioManager.instance.PlayErrorSoundAtPosition(transform.position);
                    return;
                }
                break;
            case PipetteState.Serum:
                if (state == CircleState.Empty || state == CircleState.Erythrocyte)
                    additive = new SerumAdditive(_currentBloodSample);
                else
                {
                    AudioManager.instance.PlayErrorSoundAtPosition(transform.position);
                    return;
                }
                break;
            case PipetteState.Reagent:
                if ((_currentReagent.reagentType == ReagentType.Colyclone && state == CircleState.FormedElements) 
                    || _currentReagent.reagentType == ReagentType.Erythrocyte && state == CircleState.Serum 
                    || state == CircleState.Empty) 
                    additive = new ReagentAdditive(_currentReagent);
                else
                {
                    AudioManager.instance.PlayErrorSoundAtPosition(transform.position);
                    return;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        additive?.Apply(_targetedCircle);

        _usesLeft--;
        if (_usesLeft <= 0) ResetPipette();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Circle"))
        {
            _targetedCircle = other.gameObject.GetComponent<TabletCircle>();
        }
        else if (_currentState == PipetteState.Empty && !other.CompareTag("Untagged"))
        {
            SetMaterialBasedOnTag(other.tag, other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Circle")) _targetedCircle = null;
    }

    private void SetMaterialBasedOnTag(string currentTag, Collider other)
    {
        var testTube = other.GetComponentInParent<TestTube>();
        if (testTube != null)
        {
            _currentBloodSample = testTube.bloodSample;
        }
        
        switch (currentTag)
        {
            case "Serum":
                _currentState = PipetteState.Serum;
                other.gameObject.SetActive(false);
                break;
            case "FormedElements":
                _currentState = PipetteState.FormedElements;
                other.gameObject.SetActive(false);
                break;
            case "Reagent":
                _currentReagent = other.GetComponent<Reagent>();
                _currentState = PipetteState.Reagent;
                break;
            default:
                return;
        }
        _pipetteVisual.UpdateMaterial(_currentState);
    }
    
    private void ResetPipette()
    {
        _currentReagent = null;
        _currentState = PipetteState.Empty;
        _usesLeft = 3;
        _pipetteVisual.UpdateMaterial(_currentState);
    }
}

public enum PipetteState
{
    Empty,
    FormedElements,
    Serum,
    Reagent
}