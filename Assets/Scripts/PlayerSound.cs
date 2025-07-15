using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioClip jumpClip;         // 指定跳躍音效
    private AudioSource audioSource;   // 音效來源

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayJumpSound()
    {
        if (jumpClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpClip);
        }
    }
}
