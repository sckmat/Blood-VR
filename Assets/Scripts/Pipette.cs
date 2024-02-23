using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Pipette : MonoBehaviour
{
    private PipetteState _currentState = PipetteState.Empty;

    [SerializeField] private GameObject shapedElements;
    [SerializeField] private GameObject serum;

    private MeshCollider _meshCollider;
    private TabletCircle _targetedCircle;
    private void Awake()
    {
        _meshCollider = GetComponent<MeshCollider>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _meshCollider.isTrigger = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _meshCollider.isTrigger = true;
            if (_targetedCircle != null && _currentState != PipetteState.Empty)
            {
                _targetedCircle.Fill(_currentState);
                Clear();
                _currentState = PipetteState.Empty;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Serum") && _currentState == PipetteState.Empty)
        {
            other.gameObject.SetActive(false);
            serum.SetActive(true);
            _currentState = PipetteState.Serum;
            _meshCollider.isTrigger = false;
        }
        
        if (other.CompareTag("ShapedElements") && _currentState == PipetteState.Empty)
        {
            other.gameObject.SetActive(false);
            shapedElements.SetActive(true);
            _currentState = PipetteState.ShapedElements;
            _meshCollider.isTrigger = false;
        }
        
        if (other.gameObject.CompareTag("Circle"))
        {
            Debug.Log("Circle enter");
            _targetedCircle = other.gameObject.GetComponent<TabletCircle>();
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

    private void Clear()
    {
        switch (_currentState)
        {
            case PipetteState.Serum:
                serum.SetActive(false);
                break;
            case PipetteState.ShapedElements:
                shapedElements.SetActive(false);
                break;
        }
    }
}

public enum PipetteState
{
    Empty,
    ShapedElements,
    Serum
}
