using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(Ingredient ingredient)
    {
        audioSource.clip = ingredient.AudioClip;
        audioSource.Play();
    }
}
