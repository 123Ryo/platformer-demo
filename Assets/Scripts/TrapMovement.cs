using UnityEngine;

/// <summary>
/// 控制陷阱物件上下移動，來回移動於起始位置上下的指定距離內。
/// 適用於需要持續移動的陷阱，例如上下擺動的尖刺或障礙物。
/// </summary>
public class TrapMovement : MonoBehaviour
{
    public float moveDistance = 2f;     // 移動距離
    public float moveSpeed = 2f;        // 移動速度

    private Vector3 startPos;
    private bool movingUp = true;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // 上下移動邏輯
        float newY = transform.position.y + (movingUp ? moveSpeed : -moveSpeed) * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // 到達上邊界就換方向
        if (movingUp && transform.position.y >= startPos.y + moveDistance)
        {
            movingUp = false;
        }
        //到達下邊界則改為往上移動
        else if (!movingUp && transform.position.y <= startPos.y - moveDistance)
        {
            movingUp = true;
        }
    }
}
