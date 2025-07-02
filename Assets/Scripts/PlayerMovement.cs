using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    // 地面檢查相關
    public Transform groundCheck;         // 指定腳底的檢查點（空物件）
    public float checkRadius = 0.2f;      // 檢查半徑
    public LayerMask groundLayer;         // Ground 圖層

    private Rigidbody2D rb;
    private Animator animator;

    private float moveInput;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ⬅️➡️ 移動輸入（-1 / 0 / 1）
        moveInput = Input.GetAxisRaw("Horizontal");

        // 套用水平速度
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // 設定動畫參數：是否跑步
        animator.SetBool("isRunning", moveInput != 0);

        // 翻轉角色面向
        if (moveInput != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = moveInput > 0 ? 1 : -1;
            transform.localScale = scale;
        }

        // ✅ 地面檢查（角色是否站在地上）
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        // ✅ 設定動畫參數
        animator.SetBool("isJumping", !isGrounded);        // 離地 = 跳躍中
        animator.SetFloat("yVelocity", rb.velocity.y);     // 控制 Jump_Up / Jump_Down 切換
    }
}
