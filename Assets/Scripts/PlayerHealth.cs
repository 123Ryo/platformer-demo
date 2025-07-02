using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public Image[] heartImages;

    public GameObject restartButton;   // 拖入重新開始按鈕
    public GameObject gameOverText;    // 拖入 Game Over UI 文字

    public Transform respawnPoint;     // 對岸的復活位置（記得從 Inspector 拖入）

    private PlayerController playerController;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    private bool isRespawning = false; // 防止重複掉水觸發

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        playerController = GetComponent<PlayerController>();
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();

        if (restartButton != null)
            restartButton.SetActive(false);

        if (gameOverText != null)
            gameOverText.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = i < currentHealth;
        }
    }

    void Die()
    {
        Debug.Log("角色死亡");

        if (gameOverText != null)
            gameOverText.SetActive(true);

        if (restartButton != null)
            restartButton.SetActive(true);

        if (playerController != null)
            playerController.enabled = false;

        if (playerMovement != null)
            playerMovement.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water") && !isRespawning)
        {
            StartCoroutine(HandleFallIntoWater());
        }
    }

    IEnumerator HandleFallIntoWater()
    {
        isRespawning = true;

        Debug.Log("掉進河水");

        // 停止角色操作
        if (playerController != null)
            playerController.enabled = false;

        if (playerMovement != null)
            playerMovement.enabled = false;

        // 角色沉下去
        rb.velocity = Vector2.zero;
        rb.gravityScale = 3f;

        yield return new WaitForSeconds(0.8f); // 等角色沉一下

        // 扣血
        TakeDamage(1);

        // 傳送角色回復活點
        if (respawnPoint != null)
            transform.position = respawnPoint.position;

        // 重置速度與重力
        rb.velocity = Vector2.zero;
        rb.gravityScale = 1f;

        yield return new WaitForSeconds(0.3f); // 短暫停頓

        // 如果角色還沒死，恢復控制
        if (currentHealth > 0)
        {
            if (playerController != null)
                playerController.enabled = true;

            if (playerMovement != null)
                playerMovement.enabled = true;
        }

        isRespawning = false;
    }
}
