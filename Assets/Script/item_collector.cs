using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class item_collector : MonoBehaviour
{
    // cherry数量存储在这个私有变量中
    [SerializeField] private int cherries = 0;

    [SerializeField] private Text cherriesText;

    // 当碰到樱桃时增加数量
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            Destroy(collision.gameObject);
            cherries++;
            cherriesText.text = "Cherries: " + cherries;
        }
    }

    // 获取当前樱桃数量的方法
    public int GetCherries()
    {
        return cherries;
    }

    // 消耗樱桃的方法
    public void ConsumeCherry()
    {
        if (cherries > 0)
        {
            cherries--;
            cherriesText.text = "Cherries: " + cherries;
        }
    }
}
