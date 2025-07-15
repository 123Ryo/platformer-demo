using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 遊戲管理器，負責追蹤玩家收集硬幣數量、判斷通關條件並控制 UI 顯示。
/// 通關後會顯示勝利文字與重新開始按鈕，並禁用玩家控制。
/// </summary>
public class GameManager : MonoBehaviour
{
    public int coinsCollected = 0;      // 已收集的硬幣數量
    public int coinsToWin = 10;         // 通關所需硬幣數量

    public GameObject winTextObject;         // 通關顯示文字物件
    public GameObject restartButtonObject;   // 重新開始按鈕物件

    /// <summary>
    /// 遊戲開始時初始化 UI 狀態，隱藏通關文字與重新開始按鈕。
    /// </summary>
    void Start()
    {
        if (winTextObject != null)
            winTextObject.SetActive(false);

        if (restartButtonObject != null)
            restartButtonObject.SetActive(false);
    }

    /// <summary>
    /// 玩家收集硬幣時呼叫，增加硬幣計數並判斷是否達成通關條件。
    /// </summary>
    public void AddCoin()
    {
        coinsCollected++;
        Debug.Log("硬幣數：" + coinsCollected);

        if (coinsCollected >= coinsToWin)
        {
            WinGame();
        }
    }

    /// <summary>
    /// 達成通關條件時呼叫，顯示通關 UI，禁用玩家控制。
    /// </summary>
    void WinGame()
    {
        Debug.Log("恭喜！你通關了！");

        if (winTextObject != null)
            winTextObject.SetActive(true);

        if (restartButtonObject != null)
            restartButtonObject.SetActive(true);

        // 禁用玩家控制（使用 PlayerControllerFull 腳本）
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerControllerFull controllerFull = player.GetComponent<PlayerControllerFull>();
            if (controllerFull != null)
                controllerFull.enabled = false;
        }
    }

    /// <summary>
    /// 重新載入當前場景，實現遊戲重置。
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
