using UnityEngine;

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

        // 到達上下邊界就換方向
        if (movingUp && transform.position.y >= startPos.y + moveDistance)
        {
            movingUp = false;
        }
        else if (!movingUp && transform.position.y <= startPos.y - moveDistance)
        {
            movingUp = true;
        }
    }
}
