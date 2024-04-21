using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [SerializeField] private AudioClip errorSound;
    [SerializeField] private AudioClip doneSound;
    // [SerializeField] private AudioClip centSound;
    // [SerializeField] private AudioClip rodSound;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private static void PlaySoundAtPosition(AudioClip clip, Vector3 position)
    {
        var soundObject = new GameObject("Sound")
        {
            transform =
            {
                position = position
            }
        }; 

        var audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(soundObject, clip.length);
    }

    public void PlayErrorSoundAtPosition(Vector3 position)
    {
        PlaySoundAtPosition(errorSound, position);
    }

    public void PlayDoneSoundAtPosition(Vector3 position)
    {
        PlaySoundAtPosition(doneSound, position);
    }
    
    /*public void PlayRodSoundAtPosition(Vector3 position)
    {
        PlaySoundAtPosition(rodSound, position);
    }

    public void PlayCentSoundAtPosition(Vector3 position)
    {
        PlaySoundAtPosition(centSound, position);
    }*/
}