using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToInventoryAction : Action
{
    public Inventory Inventory;

    public override void Act()
    {
        AddToInventory();
    }

    private void AddToInventory()
    {
        if (target != null)
        {
            InventoryInteractable interactableObj = target.GetComponent<InventoryInteractable>();
            if (interactableObj != null)
                Inventory.AddToEmptySlot(interactableObj);
        }

        // Remove the reference to the previous obejct
        target = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        target = collision.gameObject;
        Debug.Log("Target: " + target);
    }
}
