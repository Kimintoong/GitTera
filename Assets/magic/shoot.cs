
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab; // 子弹Prefab
    public float bulletSpeed = 10f; // 子弹速度

    void Update()
    {
        // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 获取鼠标位置在世界坐标中的位置
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // 设置z轴为0，以保持在2D平面上

        // 计算朝向和发射方向
        Vector2 shootDirection = (mousePosition - transform.position).normalized;

        // 创建子弹并设置其位置
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // 设置子弹的速度
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = shootDirection * bulletSpeed;
    }
}