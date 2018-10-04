using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class InventoryInteractable : Interactable
{
    public float PercentageValue = .2f;
    private float lambSauce; // Gordon Ramsay wants the lamb sauce, cuz its raw

    private void Start()
    {
        lambSauce = 1 - PercentageValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If collides with player
        if (collision.gameObject.layer == 8)
        {
            Movement movement = collision.gameObject.GetComponent<Movement>();
            movement.xSpeed -= (movement.xSpeed * PercentageValue);
            movement.ySpeed -= (movement.ySpeed * PercentageValue);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If collides with player
        if (collision.gameObject.layer == 8)
        {
            Movement movement = collision.gameObject.GetComponent<Movement>();
            movement.xSpeed = (movement.xSpeed / lambSauce);
            movement.ySpeed = (movement.ySpeed / lambSauce);
        }
    }
}
