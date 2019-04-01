using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FallingPlasticController : MonoBehaviour
{
    public GameEvent onPickup;

    private Rigidbody2D rb;
    private SpriteOutline spriteOutline;
    private bool playerIsPickingUp;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteOutline = GetComponent<SpriteOutline>();
    }

    public void PickUp()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    public void ShowOutline(bool isPlayerPickup = true)
    {
        if (isPlayerPickup) // If it's the player picking up plastic...
        {
            spriteOutline.color = Color.cyan;
            playerIsPickingUp = true;
        }
        else if (!isPlayerPickup && !playerIsPickingUp) // else it's the turtle choking on this plastic and player is not trying to pick it up
        {
            spriteOutline.color = new Color(191f, 0f, 0f);
        }

        spriteOutline.enabled = true;
    }

    public void ResetObject()
    {
        spriteOutline.enabled = false;
        playerIsPickingUp = false;
    }
}

