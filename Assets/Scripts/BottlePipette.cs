using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BottlePipette : MonoBehaviour
{
    private ReagentType _currentReagent = ReagentType.None;
    [SerializeField] private GameObject reagentObj;

    private MeshCollider _meshCollider;
    private Rigidbody _rb;
    private TabletCircle _targetedCircle;
    private int _usesLeft = 3;


    private void Awake()
    {
        _meshCollider = GetComponent<MeshCollider>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) && _currentReagent == ReagentType.None)
        {
            _meshCollider.isTrigger = true;
        }

        if (Input.GetKeyDown(KeyCode.Space)  && _usesLeft > 0)
        {
            _meshCollider.isTrigger = true;
            _rb.isKinematic = true;
            if (_targetedCircle != null)
            {
                _targetedCircle.AddFromReagent(_currentReagent);
                _usesLeft--;
                if (_usesLeft <= 0)
                {
                    _currentReagent = ReagentType.None;
                    reagentObj.SetActive(false);
                    _usesLeft = 3;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reagent") && _currentReagent == ReagentType.None)
        {
            Reagent reagent = other.GetComponent<Reagent>();
            if (reagent != null)
            {
                _currentReagent = reagent.GetReagent();
                reagentObj.SetActive(true);
                Debug.Log($"{_currentReagent}");
            }
        }
        
        if (other.gameObject.CompareTag("Circle"))
        {
            Debug.Log("Circle enter");
            _targetedCircle = other.gameObject.GetComponent<TabletCircle>();
            _rb.isKinematic = false;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Circle"))
        {
            Debug.Log("Circle exit");
            _targetedCircle = null;
        }
    }
}
