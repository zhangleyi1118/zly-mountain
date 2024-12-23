using UnityEngine;
using UnityEngine.Tilemaps;

public class WallBreaker : MonoBehaviour
{
    public float speedThreshold = 10f;  // �趨һ���ٶ���ֵ������ɫ�ٶȳ�����ֵʱ�ƻ�ש��
    public Tilemap tilemap;  // ���� Tilemap
    public Tile brokenTile;  // ���ƻ����ש��ͼ������ѡ��

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ����ɫ��ǽ����ײʱ���ٶ�
        if (collision.relativeVelocity.magnitude > speedThreshold)
        {
            // ��⵽��ǽ�����ײ���ٶȴ�����ֵ����ʼ�ƻ�ǽ���
            DestroyTileAtContact(collision.contacts[0].point);
        }
    }

    // ������ײλ�����ƻ���Ӧ�� Tile
    void DestroyTileAtContact(Vector2 contactPoint)
    {
        // ��ȡ����ռ��е���ײ�㣬��ת��Ϊ Tilemap �ռ��е�����
        Vector3Int tilePosition = tilemap.WorldToCell(contactPoint);

        // ����λ���Ƿ����ש�飬���������ɾ��
        if (tilemap.HasTile(tilePosition))
        {
            // ���ٸ�ש��
            tilemap.SetTile(tilePosition, null); // ��ѡ���滻Ϊ�ƻ����ש��ͼ��
            // ����ֱ���� tilemap.SetTile(tilePosition, null); ��ɾ��ש��
        }
    }
}

