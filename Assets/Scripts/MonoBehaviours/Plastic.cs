using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plastic : MonoBehaviour
{
    public PlasticData PlasticData;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = PlasticData.Sprite;
    }
}
