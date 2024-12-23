using UnityEngine;
using UnityEngine.Tilemaps;

public class WallBreaker : MonoBehaviour
{
    public float speedThreshold = 10f;  // 设置一个速度阈值，当角色速度超过这个阈值时会破坏砖块
    public Tilemap tilemap;  // 砖块的 Tilemap
    public Tile brokenTile;  // 被破坏的砖块的图片

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 只考虑竖直方向的速度分量的绝对值
        float verticalSpeed = Mathf.Abs(collision.relativeVelocity.y);
        
        if (verticalSpeed > speedThreshold)
        {
            DestroyTileAtContact(collision.contacts[0].point);
        }
    }

    // 根据碰撞点的位置，销毁对应的 Tile
    void DestroyTileAtContact(Vector2 contactPoint)
    {
        // 获取碰撞点的世界坐标，转换为 Tilemap 的局部坐标
        Vector3Int tilePosition = tilemap.WorldToCell(contactPoint);

        // 判断该位置是否有砖块，如果有则销毁
        if (tilemap.HasTile(tilePosition))
        {
            // 销毁砖块
            tilemap.SetTile(tilePosition, null); // 将选中的砖块替换为被破坏的砖块图片
            // 直接使用 tilemap.SetTile(tilePosition, null); 销毁砖块
        }
    }
}

