using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] InteractableObject[] Bases;
    protected SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        int randomIndex = Random.Range(0, Bases.Length);
        spriteRenderer.sprite = Bases[randomIndex].Sprite;
    }
}
