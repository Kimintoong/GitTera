
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab; // �ӵ�Prefab
    public float bulletSpeed = 10f; // �ӵ��ٶ�

    void Update()
    {
        // ������������
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // ��ȡ���λ�������������е�λ��
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // ����z��Ϊ0���Ա�����2Dƽ����

        // ���㳯��ͷ��䷽��
        Vector2 shootDirection = (mousePosition - transform.position).normalized;

        // �����ӵ���������λ��
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // �����ӵ����ٶ�
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = shootDirection * bulletSpeed;
    }
}