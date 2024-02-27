using UnityEngine;
using UnityEngine.Serialization;

public class TestTube : MonoBehaviour
{
    public enum BloodState { WholeBlood, Centrifuged }

    public BloodState currentState;

    [SerializeField] private GameObject wholeBlood;
    [SerializeField] private GameObject centrifugedBlood;

    public void SetBloodState(BloodState state)
    {
        Debug.Log(state);
        currentState = state;
        wholeBlood.SetActive(state == BloodState.WholeBlood);
        centrifugedBlood.SetActive(state == BloodState.Centrifuged);
    }

    public BloodState GetBloodState()
    {
        return currentState;
    }
    
    // void Start()
    // {
    //     SetBloodState(BloodState.WholeBlood);
    // }
}
