using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Add this component to objects with the AudioSource component containing the BGM or Ambient clips.
// Make sure to set what type of audio it is

[RequireComponent(typeof(AudioSource))]

public class AudioSwitcher : MonoBehaviour
{
    [SerializeField] private AudioType audioType;
    private AudioSource audioSource;
    private static PersistentAudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<PersistentAudioManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // Turn off for now; this is more of a workaround since audio clips are already set up in most scenes
        audioSource.enabled = false;

        if (audioSource.clip == null)
        {
            Debug.LogWarning("Audio clip is null");
            return;
        }

        // Check
        AudioClip currentPlayingClip = audioManager.AudioSource(audioType).clip;
        if (audioSource.clip != currentPlayingClip || currentPlayingClip == null)
        {
            audioManager.SwitchAudio(audioSource.clip, audioType);
        }
    }
}

public enum AudioType
{
    BGM,
    Ambient
}
