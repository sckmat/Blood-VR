using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Pipette : MonoBehaviour
{
    private enum PipetteState
    {
        Empty,
        ShapedElements,
        Serum
    }

    private PipetteState _currentState = PipetteState.Empty;

    [SerializeField] private GameObject shapedElements;
    [SerializeField] private GameObject serum;

    private MeshCollider _meshCollider;
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Serum") && _currentState == PipetteState.Empty)
        {
            other.gameObject.SetActive(false);
            serum.SetActive(true);
            _currentState = PipetteState.ShapedElements;
            _meshCollider.isTrigger = false;
        }
        
        if (other.CompareTag("ShapedElements") && _currentState == PipetteState.Empty)
        {
            other.gameObject.SetActive(false);
            shapedElements.SetActive(true);
            _currentState = PipetteState.Serum;
            _meshCollider.isTrigger = false;
        }
    }
}
