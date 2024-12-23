using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class item_collector : MonoBehaviour
{
    // 记录收集到的樱桃数量
    private int cherries = 0;

    [SerializeField] private Text cherriesText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 当碰到樱桃时，销毁樱桃并增加樱桃数量
        if (collision.gameObject.CompareTag("Cherry"))
        {
            Destroy(collision.gameObject);
            cherries++;
            cherriesText.text = "Cherries: " + cherries;
        }
    }

    // 获取当前樱桃数量
    public int GetCherries()
    {
        return cherries;
    }

    // 消耗一个樱桃
    public void ConsumeCherry()
    {
        if (cherries > 0)
        {
            cherries--;
            cherriesText.text = "Cherries: " + cherries;
        }
    }
}
