using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2f; // 子弹生命周期

    void Start()
    {
        // 在指定时间后销毁子弹
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // 检查是否与玩家发生碰撞
        if (collider.CompareTag("Player"))
        {
            // 与玩家碰撞时，不执行任何操作
            return;
        }
    }
}