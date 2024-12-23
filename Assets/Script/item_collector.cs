using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class item_collector : MonoBehaviour
{
    // ��¼�ռ�����ӣ������
    private int cherries = 0;

    [SerializeField] private Text cherriesText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ������ӣ��ʱ������ӣ�Ҳ�����ӣ������
        if (collision.gameObject.CompareTag("Cherry"))
        {
            Destroy(collision.gameObject);
            cherries++;
            cherriesText.text = "Cherries: " + cherries;
        }
    }

    // ��ȡ��ǰӣ������
    public int GetCherries()
    {
        return cherries;
    }

    // ����һ��ӣ��
    public void ConsumeCherry()
    {
        if (cherries > 0)
        {
            cherries--;
            cherriesText.text = "Cherries: " + cherries;
        }
    }
}
