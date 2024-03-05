using UnityEngine;

public class Reagent : MonoBehaviour
{
    public ReagentType reagentType = ReagentType.None;
    public Colyclone colyclone = Colyclone.None;
    public Erythrocyte erythrocyte= Erythrocyte.None;
    private void Awake()
    {
        DisplayReagent(); 
    }

    private void DisplayReagent()
    {
        //todo изменять внешний вид реагента в бутылке и этикетку
    }
}

public enum ReagentType { None, Colyclone, Erythrocyte }
public enum Colyclone { None, AntiA, AntiB, AntiD }
public enum Erythrocyte { None, ErythrocyteA, ErythrocyteB, ErythrocyteO }

