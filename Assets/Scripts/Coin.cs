using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameManager gameManager;
    private CoinSound coinSound; // 🔸 新增：播放收集音效的腳本變數

    private void Start()
    {
        // 🔹 自動尋找場景中的 GameManager
        gameManager = GameObject.FindObjectOfType<GameManager>();

        // 🔹 嘗試從自己身上抓取 CoinSound 腳本並把 CoinSound.cs 加在同一個物件上）
        coinSound = GetComponent<CoinSound>();

        if (gameManager == null)
        {
            Debug.LogWarning("找不到 GameManager，請確認場景中有 GameManager 物件！");
        }

        if (coinSound == null)
        {
            Debug.LogWarning("找不到 CoinSound 腳本，請確認 Coin 預製件上有掛載！");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 🔹 確認碰到的是玩家（Tag 設成 Player）
        if (other.CompareTag("Player"))
        {
            // 🔹 通知 GameManager 增加硬幣數量
            if (gameManager != null)
            {
                gameManager.AddCoin();
            }

            // 🔹 播放收集音效（如果有掛載 CoinSound 腳本）
            if (coinSound != null)
            {
                coinSound.PlayCollectSound();
            }

            // 🔹 稍微延遲再刪除硬幣，避免音效還沒播完就被銷毀
            Destroy(gameObject, 0.1f);
        }
    }
}
