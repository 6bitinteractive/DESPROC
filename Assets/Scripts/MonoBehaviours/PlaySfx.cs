using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PlaySfx : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPlasticPickupSfx(Interactable interactableObject)
    {
        PlasticInteractable plasticObject = interactableObject as PlasticInteractable;

        audioSource.clip = plasticObject.GetPickupSfx();
        if (audioSource.clip != null)
            audioSource.Play();
    }
}
