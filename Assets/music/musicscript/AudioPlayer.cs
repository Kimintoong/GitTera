using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        // 获取AudioSource组件
        audioSource = GetComponent<AudioSource>();
        // 播放背景音乐
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
