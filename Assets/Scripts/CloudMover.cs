using UnityEngine;

/// <summary>
/// 控制雲朵持續向左移動，並在離開畫面後從右邊重新出現，達到重複漂移的視覺效果。
/// </summary>
public class CloudMover : MonoBehaviour
{
    [Header("移動設定")]
    public float speed = 1f;              // 每秒水平移動速度，數值越大移動越快
    public float resetPositionX = 10f;    // 雲朵從畫面右側重新出現的位置 X
    public float leftLimitX = -10f;       // 雲朵完全離開畫面左側的界線 X（觸發重置）

    void Update()
    {
        // ✅ 雲朵每禎向左移動
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // ✅ 如果雲朵超出左邊邊界，就將它的位置重設到右邊起始點，讓它循環漂浮
        if (transform.position.x < leftLimitX)
        {
            Vector3 newPos = transform.position;
            newPos.x = resetPositionX;
            transform.position = newPos;
        }
    }
}
