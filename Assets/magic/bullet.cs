using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2f; // �ӵ���������

    void Start()
    {
        // ��ָ��ʱ��������ӵ�
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // ����Ƿ�����ҷ�����ײ
        if (collider.CompareTag("Player"))
        {
            // �������ײʱ����ִ���κβ���
            return;
        }
    }
}