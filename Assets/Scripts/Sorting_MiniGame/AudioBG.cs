using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBG : MonoBehaviour
{
    AudioSource audioSource;

    static bool AudioBegin = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (!AudioBegin)
        {
            audioSource.Play();
            DontDestroyOnLoad(gameObject);
            AudioBegin = true;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
