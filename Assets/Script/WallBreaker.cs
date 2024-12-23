using UnityEngine;
using UnityEngine.Tilemaps;

public class WallBreaker : MonoBehaviour
{
    public float speedThreshold = 10f;  // 设定一个速度阈值，当角色速度超过此值时破坏砖块
    public Tilemap tilemap;  // 引用 Tilemap
    public Tile brokenTile;  // 被破坏后的砖块图案（可选）

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查角色与墙体碰撞时的速度
        if (collision.relativeVelocity.magnitude > speedThreshold)
        {
            // 检测到与墙体的碰撞且速度大于阈值，开始破坏墙体块
            DestroyTileAtContact(collision.contacts[0].point);
        }
    }

    // 根据碰撞位置来破坏对应的 Tile
    void DestroyTileAtContact(Vector2 contactPoint)
    {
        // 获取世界空间中的碰撞点，并转换为 Tilemap 空间中的坐标
        Vector3Int tilePosition = tilemap.WorldToCell(contactPoint);

        // 检查该位置是否存在砖块，如果存在则删除
        if (tilemap.HasTile(tilePosition))
        {
            // 销毁该砖块
            tilemap.SetTile(tilePosition, null); // 可选：替换为破坏后的砖块图案
            // 或者直接用 tilemap.SetTile(tilePosition, null); 来删除砖块
        }
    }
}

