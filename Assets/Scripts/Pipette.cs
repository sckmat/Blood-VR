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
    private Rigidbody _rb; 
    private int _usesLeft = 3;

    private void Awake()
    {
        _meshCollider = GetComponent<MeshCollider>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _meshCollider.isTrigger = true;
            _rb.isKinematic = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && _usesLeft > 0)
        {
            _meshCollider.isTrigger = true;
            _rb.isKinematic = true;
            if (_targetedCircle != null && _currentState != PipetteState.Empty)
            {
                _targetedCircle.Fill(_currentState);
                _usesLeft--;
                if (_usesLeft <= 0)
                {
                    Clear();
                    _currentState = PipetteState.Empty;
                    _usesLeft = 3;
                }
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
            _rb.isKinematic = false;
        }
        
        if (other.CompareTag("ShapedElements") && _currentState == PipetteState.Empty)
        {
            other.gameObject.SetActive(false);
            shapedElements.SetActive(true);
            _currentState = PipetteState.ShapedElements;
            _meshCollider.isTrigger = false;
            _rb.isKinematic = false;
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
