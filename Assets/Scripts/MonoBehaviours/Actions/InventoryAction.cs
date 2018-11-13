using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// TODO: Separate TrashPickup and EmptyBin actions; inherit from InventoryAction?

public class InventoryAction : Action
{
    public Inventory Inventory;

    public override void Act()
    {
        if (target == null)
            return;

        InventoryInteractable interactableObj = target.GetComponent<InventoryInteractable>();
        if (interactableObj != null)
        {
            AddToInventory(interactableObj);
        }
        else if (target.GetComponent<Bin>() != null)
        {
            Inventory.Empty();
            Debug.Log("Empty inventory.");
        }
    }

    private void AddToInventory(InventoryInteractable interactableObj)
    {
        if (interactableObj != null)
            Inventory.AddToEmptySlot(interactableObj);

        // Remove the reference to the previous obejct
        target = null;
    }
}
