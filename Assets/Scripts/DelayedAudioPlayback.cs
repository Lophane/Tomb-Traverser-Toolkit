using UnityEngine;

public class DelayedAudioPlayback : MonoBehaviour
{
    public AudioSource audioSource;
    public float delay = 2.1f;

    private void Start()
    {
        // Call the PlayDelayed method after the specified delay
        Invoke("PlayDelayed", delay);
    }

    private void PlayDelayed()
    {
        // Play the audio source
        audioSource.Play();
    }
}