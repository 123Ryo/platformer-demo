using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int coinsCollected = 0;
    public int coinsToWin = 10;

    public GameObject winTextObject;         // 通關文字
    public GameObject restartButtonObject;   // 重新開始按鈕

    void Start()
    {
        if (winTextObject != null)
            winTextObject.SetActive(false);  // 隱藏通關文字

        if (restartButtonObject != null)
            restartButtonObject.SetActive(false);  // 隱藏按鈕
    }

    public void AddCoin()
    {
        coinsCollected++;
        Debug.Log("硬幣數：" + coinsCollected);

        if (coinsCollected >= coinsToWin)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        Debug.Log("恭喜！你通關了！");

        // 顯示 UI
        if (winTextObject != null)
            winTextObject.SetActive(true);

        if (restartButtonObject != null)
            restartButtonObject.SetActive(true);

        // ⛔ 禁用角色控制
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            PlayerController controller = player.GetComponent<PlayerController>();
            PlayerMovement movement = player.GetComponent<PlayerMovement>();

            if (controller != null)
                controller.enabled = false;

            if (movement != null)
                movement.enabled = false;
        }
    }

    // 給按鈕用的函式：重新開始遊戲
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TestClick()
    {
        Debug.Log("按鈕有被點到！");
    }
}
