using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        // 初始值設定成目前 AudioListener 音量
        volumeSlider.value = AudioListener.volume;

        // 加上滑桿事件
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void SetVolume(float value)
    {
        AudioListener.volume = value;
    }
}
