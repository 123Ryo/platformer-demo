using UnityEngine;

/// <summary>
/// 控制玩家角色的移動、跳躍與動畫狀態。
/// 支援二段跳，包含地面偵測與角色翻轉邏輯。
/// 適用於2D平台遊戲中玩家的完整控制腳本。
/// </summary>
public class PlayerControllerFull : MonoBehaviour
{
    [Header("移動與跳躍")]
    public float moveSpeed = 5f;       // 玩家左右移動速度
    public float jumpForce = 7f;       // 跳躍施加的向上力道
    public int maxJumps = 2;           // 最大跳躍次數（例如二段跳為2）

    [Header("地面偵測")]
    public Transform groundCheck;          // 腳底判定點（需在 Inspector 指定）
    public float groundCheckRadius = 0.2f; // 判定半徑，用於判斷是否接觸地面
    public LayerMask groundLayer;          // 定義哪些圖層被視為地面

    private Rigidbody2D rb;               // 玩家剛體，控制物理行為
    private Animator animator;            // 控制動畫播放
    private PlayerSound playerSound;      // 播放玩家音效的腳本

    private bool isGrounded = false;      // 玩家當前是否站在地面
    private bool wasGrounded = false;     // 玩家上一幀是否站在地面，用於判斷剛落地
    private int jumpCount = 0;            // 已經跳了幾次，限制跳躍次數

    /// <summary>
    /// 初始化組件參考。
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();         // 取得玩家剛體元件
        animator = GetComponent<Animator>();      // 取得動畫元件
        playerSound = GetComponent<PlayerSound>(); // 取得音效控制腳本
    }

    /// <summary>
    /// 每幀更新：處理玩家移動、跳躍輸入及動畫參數更新。
    /// </summary>
    void Update()
    {
        HandleMovement();   // 控制水平移動與角色翻轉
        HandleJump();       // 監聽跳躍按鍵並執行跳躍
        HandleAnimation();  // 根據狀態更新動畫參數
    }

    /// <summary>
    /// 固定時間間隔更新：判斷玩家是否接觸地面，並根據落地情況重置跳躍計數。
    /// </summary>
    void FixedUpdate()
    {
        wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // 如果剛剛從空中落地，重置跳躍次數
        if (!wasGrounded && isGrounded)
        {
            jumpCount = 0;
        }
    }

    /// <summary>
    /// 控制玩家左右移動並依移動方向翻轉角色面向。
    /// </summary>
    void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");    // 獲取左右鍵輸入 -1,0,1
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // 根據移動方向調整角色水平翻轉
        if (moveInput != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = moveInput > 0 ? 1 : -1;
            transform.localScale = scale;
        }
    }

    /// <summary>
    /// 處理跳躍輸入，並控制最大跳躍次數。執行跳躍後播放跳躍音效。
    /// </summary>
    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) && jumpCount < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;

            // 播放跳躍音效（若有掛 PlayerSound 腳本）
            if (playerSound != null)
            {
                playerSound.PlayJumpSound();
            }
        }
    }

    /// <summary>
    /// 根據移動與跳躍狀態更新動畫參數，控制跑步與跳躍動畫切換。
    /// </summary>
    void HandleAnimation()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        animator.SetBool("isRunning", moveInput != 0);   // 有水平移動時播放跑步動畫
        animator.SetBool("isJumping", !isGrounded);      // 離地時播放跳躍動畫
        animator.SetFloat("yVelocity", rb.velocity.y);   // 使用垂直速度判斷跳躍上升或下落動畫
    }

    /// <summary>
    /// 在 Scene 檢視視窗中顯示地面偵測半徑範圍，方便調整。
    /// </summary>
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
