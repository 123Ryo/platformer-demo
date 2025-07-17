using UnityEngine;

public class TrapSound : MonoBehaviour
{
    public AudioClip hitSound;              // 播放的音效
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
    }

    public void PlayHitSound()
    {
        if (hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}
