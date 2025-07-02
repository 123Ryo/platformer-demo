using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    public Transform groundCheck;          // 判斷地面的參考點（記得拖物件）
    public float groundCheckRadius = 0.2f; // 地面檢查半徑
    public LayerMask groundLayer;          // 地面圖層（設定成 Ground）

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool wasGrounded;              // 上一幀是否在地面上
    private int jumpCount = 0;
    public int maxJumps = 2;               // 可跳躍次數（2 = 二段跳）

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 左右移動
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // 跳躍判斷（用 Left Alt）
        if (Input.GetKeyDown(KeyCode.LeftAlt) && jumpCount < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
    }

    void FixedUpdate()
    {
        // 先存下上一幀的狀態
        wasGrounded = isGrounded;

        // 判斷是否站在地面
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // 只有剛落地（上一幀沒接地，這幀有接地）才重置跳躍次數
        if (!wasGrounded && isGrounded)
        {
            jumpCount = 0;
        }
    }

    // 可視化 GroundCheck 範圍，方便調整
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
