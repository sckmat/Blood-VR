using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Pipette : MonoBehaviour
{
    private PipetteState _currentState = PipetteState.Empty;
    private Dictionary<PipetteState, Material> _materials;
    private TabletCircle _targetedCircle;
    private int _usesLeft = 3;
    private ReagentType _currentReagent;

    [SerializeField] private MeshRenderer content;

    private void Awake()
    {
        _materials = new Dictionary<PipetteState, Material>
        {
            { PipetteState.Empty, Resources.Load<Material>("Materials/Empty") },
            { PipetteState.Serum, Resources.Load<Material>("Materials/Serum") },
            { PipetteState.FormedElements, Resources.Load<Material>("Materials/FormedElements") },
            { PipetteState.Reagent, Resources.Load<Material>("Materials/Colyclone") }
        };
        content.material = _materials[_currentState];
    }

    public void Activate()
    {
        if (_targetedCircle == null || _currentState == PipetteState.Empty) return;

        if (_currentState == PipetteState.Reagent)
        {
            Debug.Log("Reagent");
            _targetedCircle.FillFromReagent(_currentReagent);
        }
        else
        {
            _targetedCircle.FillFromTestTube(_currentState);
        }

        _usesLeft--;
        if (_usesLeft <= 0) ResetPipette();
    }

    private void ResetPipette()
    {
        _currentReagent = ReagentType.None;
        _currentState = PipetteState.Empty;
        _usesLeft = 3;
        content.material = _materials[_currentState];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Circle"))
        {
            _targetedCircle = other.gameObject.GetComponent<TabletCircle>();
            Debug.Log("Circle enter");
        }
        else if (_currentState == PipetteState.Empty && !other.CompareTag("Untagged"))
        {
            SetMaterialBasedOnTag(other.tag, other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Circle")) _targetedCircle = null;
        Debug.Log("Circle exit");
    }

    private void SetMaterialBasedOnTag(string currentTag, Collider other)
    {
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
                var reagentComponent = other.GetComponent<Reagent>();
                _currentReagent = reagentComponent.GetReagent();
                _currentState = PipetteState.Reagent;
                break;
            default:
                return;
        }
        content.material = _materials[_currentState];
    }
}

public enum PipetteState
{
    Empty,
    FormedElements,
    Serum,
    Reagent
}
