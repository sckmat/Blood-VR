using System.Collections.Generic;
using UnityEngine;

public class Pipette : MonoBehaviour
{
    private PipetteState _currentState = PipetteState.Empty;
    private Dictionary<PipetteState, List<Material>> _materials;
    private TabletCircle _targetedCircle;
    private int _usesLeft = 3;
    private Reagent _currentReagent;

    [SerializeField] private MeshRenderer content;

    private void Awake()
    {
        LoadMaterials();
        content.material = _materials[_currentState][0];
    }

    private void LoadMaterials()
    {
        _materials = new Dictionary<PipetteState, List<Material>>
        {
            { PipetteState.Empty, new List<Material>(Resources.LoadAll<Material>("Materials/Empty")) },
            { PipetteState.Serum, new List<Material>(Resources.LoadAll<Material>("Materials/Serum")) },
            { PipetteState.FormedElements, new List<Material>(Resources.LoadAll<Material>("Materials/FormedElements")) },
            { PipetteState.Reagent, new List<Material>(Resources.LoadAll<Material>("Materials/Colyclone")) }
        };
    }
    public void Activate()
    {
        if (_targetedCircle == null || _currentState == PipetteState.Empty) return;

        if (_currentState == PipetteState.Reagent)
        {
            Debug.Log("Reagent");
            _targetedCircle.AddFromReagent(_currentReagent);
        }
        else
        {
            _targetedCircle.AddFromTestTube(_currentState);
        }

        _usesLeft--;
        if (_usesLeft <= 0) ResetPipette();
    }

    private void ResetPipette()
    {
        _currentReagent = null;
        _currentState = PipetteState.Empty;
        _usesLeft = 3;
        content.material = _materials[_currentState][0];
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
        content.material = _materials[_currentState][0];
    }
}

public enum PipetteState
{
    Empty,
    FormedElements,
    Serum,
    Reagent
}
