using UnityEngine;

public class UISoundManager : MonoBehaviour
{
    public AudioClip clickClip;
    public AudioClip toggleClip;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClick()
    {
        if (clickClip != null)
            audioSource.PlayOneShot(clickClip);
    }

    public void PlayToggle()
    {
        if (toggleClip != null)
            audioSource.PlayOneShot(toggleClip);
    }
}
