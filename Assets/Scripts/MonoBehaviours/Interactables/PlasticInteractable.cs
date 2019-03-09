using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]

public class PlasticInteractable : Interactable
{
    [SerializeField] private Plastic plasticData;

    public Plastic PlasticData
    {
        get { return plasticData; }
    }

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = plasticData.Sprite;
    }

    public override void Interact()
    {
        OnInteract.Invoke();
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
