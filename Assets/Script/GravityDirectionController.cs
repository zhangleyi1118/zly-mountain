using UnityEngine;

public class GravityDirectionController : MonoBehaviour
{
    private Rigidbody2D rb;
    private item_collector itemCollector;  // 引用收集器脚本
    private float originalGravityScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        itemCollector = GetComponent<item_collector>();  // 获取 item_collector 脚本

        // 保存原始的重力比例
        originalGravityScale = rb.gravityScale;
    }

    void Update()
    {
        // 按下 I 键，且有足够的樱桃时才改变重力方向
        if (Input.GetKeyDown(KeyCode.I) && itemCollector.GetCherries() > 0)
        {
            rb.gravityScale = -rb.gravityScale;  // 改变重力方向

            // 消耗一个樱桃
            itemCollector.ConsumeCherry();
        }
    }
}
