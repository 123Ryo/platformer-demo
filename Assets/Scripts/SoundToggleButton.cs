using UnityEngine;
using UnityEngine.UI;

public class SoundToggleButton : MonoBehaviour
{
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    public AudioSource bgmSource;
    private Image buttonImage;

    private bool isMuted = false;

    void Start()
    {
        buttonImage = GetComponent<Image>();

        // 初始化圖片
        UpdateButtonImage();
    }

    public void ToggleSound()
    {
        isMuted = !isMuted;
        bgmSource.mute = isMuted;
        UpdateButtonImage();
    }

    void UpdateButtonImage()
    {
        if (isMuted)
        {
            buttonImage.sprite = soundOffSprite;
        }
        else
        {
            buttonImage.sprite = soundOnSprite;
        }
    }
}
