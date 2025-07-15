using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    public int damageAmount = 1; // 每次扣幾滴血

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ✅ 玩家進入陷阱的觸發區域
        if (collision.CompareTag("Player"))
        {
            // ✅ 嘗試取得玩家的血量腳本
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount); // ✅ 傷害玩家
            }

            // 🔊 播放陷阱觸碰音效（若有掛 TrapSound）
            TrapSound trapSound = GetComponent<TrapSound>();
            if (trapSound != null)
            {
                trapSound.PlayHitSound();
            }
        }
    }
}
