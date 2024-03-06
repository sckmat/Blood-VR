using UnityEngine;

public class TestTube : MonoBehaviour
{
    public BloodState currentState;
    public BloodSample bloodSample;

    [SerializeField] private GameObject wholeBlood;
    [SerializeField] private GameObject centrifugedBlood;
    
    private void Awake()
    {
        BloodManager.AddTestTube(this);
        SetBloodState(BloodState.WholeBlood);
    }
    
    public void SetBloodState(BloodState state)
    {
        currentState = state;
        wholeBlood.SetActive(state == BloodState.WholeBlood);
        centrifugedBlood.SetActive(state == BloodState.Centrifuged);
    }

    public void DestroyTestTube()
    {
        Destroy(gameObject);
    }
}

public enum BloodState { WholeBlood, Centrifuged }