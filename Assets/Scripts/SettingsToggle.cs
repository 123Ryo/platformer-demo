using UnityEngine;

public class SettingsToggle : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject player; // 🔹 這是你的玩家物件（拖進來）

    private bool isOpen = false;

    public void ToggleSettings()
    {
        isOpen = !isOpen;
        settingsPanel.SetActive(isOpen);

        // 🔹 啟用/停用玩家控制
        if (player != null)
        {
            PlayerControllerFull controller = player.GetComponent<PlayerControllerFull>();
            if (controller != null)
                controller.enabled = !isOpen;
        }

        // ✅ 可選：暫停時間
        // Time.timeScale = isOpen ? 0f : 1f;
    }
}
