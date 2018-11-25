using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [HideInInspector] public PlasticInteractable inventoryItem;
    [SerializeField] private Image image; // Note: this is sort of a hack; see comment at the end of script

    private void Start()
    {
        image.color = Color.clear; // hide image
    }

    public void UpdateImage()
    {
        image.color = Color.white; // show image
        Debug.Log(inventoryItem.GetSprite());
        image.sprite = inventoryItem.GetSprite();
    }
}

// Issue: Sprite is too small when moved to Canvas
// Reference:
// https://gamedev.stackexchange.com/questions/151448/how-do-you-calculate-a-sprites-localscale-to-resize-the-sprite-to-a-defined-hei
// https://forum.unity.com/threads/canvas-sprite-small.404082/
