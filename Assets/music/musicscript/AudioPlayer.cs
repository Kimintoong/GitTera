using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        // ��ȡAudioSource���
        audioSource = GetComponent<AudioSource>();
        // ���ű�������
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
