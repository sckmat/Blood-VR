using UnityEngine;
using UnityEngine.Serialization;

public class TestTube : MonoBehaviour
{
    public BloodState currentState;
    public static BloodSample bloodSample;

    [SerializeField] private GameObject wholeBlood;
    [SerializeField] private GameObject centrifugedBlood;
    
    private void Awake()
    {
        var randomBloodType = (BloodType)Random.Range(0, 4);
        var randomRhesusFactor = (RhesusFactor)Random.Range(0, 2);
        var randomBloodSample = new BloodSample(randomBloodType, randomRhesusFactor);
        bloodSample = randomBloodSample;
        SetBloodState(BloodState.WholeBlood);
        Debug.Log($"{bloodSample.bloodType} {bloodSample.rhesusFactor}");
    }
    
    public void SetBloodState(BloodState state)
    {
        currentState = state;
        wholeBlood.SetActive(state == BloodState.WholeBlood);
        centrifugedBlood.SetActive(state == BloodState.Centrifuged);
    }
}

public enum BloodState { WholeBlood, Centrifuged }