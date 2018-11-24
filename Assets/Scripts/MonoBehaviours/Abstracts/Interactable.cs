using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private Factory Factory;
    public int baseIndex { get; set; }

    protected SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        baseIndex = Random.Range(0, Factory.BaseObjects.Length);
        spriteRenderer.sprite = Factory.BaseObjects[baseIndex].Sprite;
    }

    public virtual InteractableObject GetInteractable()
    {
        return Factory.BaseObjects[baseIndex];
    }

    public virtual Sprite GetSprite()
    {
        return spriteRenderer.sprite;
    }

    public virtual Factory GetFactory()
    {
        return Factory;
    }
}
