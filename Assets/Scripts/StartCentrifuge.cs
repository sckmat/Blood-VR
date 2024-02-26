using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCentrifuge : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener((() => Debug.Log("start")));
    }
}
