using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Debug.Log("Empty bin.");
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
