using UnityEngine;

public class TestTube : MonoBehaviour
{
    public enum BloodState { WholeBlood, Centrifuged }

    private BloodState _currentState;

    [SerializeField] private GameObject wholeBlood;
    [SerializeField] private GameObject centrifugedBlood;

    public void SetBloodState(BloodState state)
    {
        Debug.Log(state);
        _currentState = state;
        wholeBlood.SetActive(state == BloodState.WholeBlood);
        centrifugedBlood.SetActive(state == BloodState.Centrifuged);
    }

    public BloodState GetBloodState()
    {
        return _currentState;
    }
    
    // void Start()
    // {
    //     SetBloodState(BloodState.WholeBlood);
    // }
}
