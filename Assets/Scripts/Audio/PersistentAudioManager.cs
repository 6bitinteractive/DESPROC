using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource ambient;

    public AudioSource AudioSource(AudioType audioType)
    {
        switch (audioType)
        {
            case AudioType.BGM:
                return bgm;
            case AudioType.Ambient:
                return ambient;
        }

        return null;
    }

    public void SwitchAudio(AudioClip clip, AudioType type)
    {
        AudioSource audioSource = AudioSource(type);
        audioSource.clip = clip;
        audioSource.Play();
    }
}