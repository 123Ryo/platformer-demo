using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// 管理玩家血量，包含血量扣除、血條更新、死亡判定和復活機制。
/// 玩家掉落水中會扣血並回到復活點，當血量為0時，顯示Game Over並停止控制。
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;                 // 最大血量，玩家生命數量上限
    private int currentHealth;                // 玩家當前血量，隨遊戲過程變動

    public Image[] heartImages;               // UI 心形圖片陣列，表示血量狀態

    public GameObject restartButton;          // 重新開始按鈕，死亡時顯示
    public GameObject gameOverText;           // Game Over 文字，死亡時顯示

    public Transform respawnPoint;            // 復活點，玩家掉水後傳送回此處

    private PlayerControllerFull playerControllerFull;  // 玩家控制腳本，死亡時禁用
    private Rigidbody2D rb;                  // 玩家剛體，用來控制物理行為
    private Animator animator;               // 玩家動畫控制器

    private bool isRespawning = false;       // 防止多次同時觸發掉水復活

    void Start()
    {
        currentHealth = maxHealth;           // 初始化血量為最大值
        UpdateHealthUI();                    // 初始化更新血條UI顯示

        // 取得相關元件，方便後續控制
        playerControllerFull = GetComponent<PlayerControllerFull>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // 一開始隱藏死亡相關UI
        if (restartButton != null)
            restartButton.SetActive(false);

        if (gameOverText != null)
            gameOverText.SetActive(false);
    }

    /// <summary>
    /// 外部呼叫此函式來讓玩家扣血。扣完血會更新 UI，若血量歸零則執行死亡流程。
    /// </summary>
    /// <param name="amount">扣除的血量數值</param>
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // 限制血量不超過範圍
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// 根據 currentHealth 更新 UI 中的心形顯示，血量低於的心形隱藏。
    /// </summary>
    void UpdateHealthUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = i < currentHealth;
        }
    }

    /// <summary>
    /// 玩家死亡時呼叫，播放死亡動畫，顯示死亡UI，並停用玩家控制及剛體移動。
    /// </summary>
    void Die()
    {
        Debug.Log("角色死亡");

        if (animator != null)
        {
            animator.SetTrigger("DieTrigger");   // 播放死亡動畫
        }

        if (gameOverText != null)
            gameOverText.SetActive(true);       // 顯示死亡文字

        if (restartButton != null)
            restartButton.SetActive(true);      // 顯示重新開始按鈕

        if (playerControllerFull != null)
            playerControllerFull.enabled = false;  // 停用玩家控制腳本

        rb.velocity = Vector2.zero;             // 停止物理運動
    }

    /// <summary>
    /// 玩家進入水域時觸發，啟動掉水復活流程，防止連續觸發。
    /// </summary>
    /// <param name="collision">碰撞物件</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water") && !isRespawning)
        {
            StartCoroutine(HandleFallIntoWater());
        }
    }

    /// <summary>
    /// 掉入水中時的流程協程：暫時停用控制，增加重力讓角色沉入水中，扣血並傳送回復活點，恢復控制。
    /// </summary>
    /// <returns>IEnumerator</returns>
    IEnumerator HandleFallIntoWater()
    {
        isRespawning = true;

        Debug.Log("掉進河水");

        if (playerControllerFull != null)
            playerControllerFull.enabled = false;   // 停用控制

        rb.velocity = Vector2.zero;
        rb.gravityScale = 3f;   // 增加重力讓角色沉下去

        yield return new WaitForSeconds(0.8f); // 等待角色沉沒動畫或效果

        TakeDamage(1);  // 掉水扣血

        if (respawnPoint != null)
            transform.position = respawnPoint.position;  // 傳送回復活點

        rb.velocity = Vector2.zero;
        rb.gravityScale = 1f;   // 重置重力為原始值

        yield return new WaitForSeconds(0.3f);  // 短暫暫停讓動作更自然

        if (currentHealth > 0)
        {
            if (playerControllerFull != null)
                playerControllerFull.enabled = true;    // 恢復控制
        }

        isRespawning = false;
    }
}
