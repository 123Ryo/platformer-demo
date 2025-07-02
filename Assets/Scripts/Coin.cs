using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        // 自動尋找場景中的 GameManager（只需有一個）
        gameManager = GameObject.FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogWarning("找不到 GameManager，請確認場景中有 GameManager 物件！");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 確認碰到的是玩家（Tag 記得設成 Player）
        if (other.CompareTag("Player"))
        {
            if (gameManager != null)
            {
                gameManager.AddCoin();  // 通知 GameManager 增加硬幣數量
            }

            Destroy(gameObject);  // 撞到後銷毀自己（硬幣）
        }
    }
}
