using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reagent : MonoBehaviour
{
    [SerializeField] private ReagentType reagentType = ReagentType.None;
    
    public ReagentType GetReagent()
    {
        return reagentType;
    }

    private void DisplayReagent()
    {
        //todo изменять внешний вид реагента в бутылке
    }

    private void Awake()
    {
        DisplayReagent(); 
    }
}

public enum ReagentType { None, AntiA, AntiB, AntiD }
