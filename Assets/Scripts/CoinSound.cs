using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CoinSound : MonoBehaviour
{
    public AudioClip collectSound;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayCollectSound()
    {
        if (collectSound != null)
        {
            audioSource.PlayOneShot(collectSound);
        }
    }
}
