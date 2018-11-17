using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AddToInventoryAction : InventoryAction
{
    public override void Act()
    {
        if (target == null)
            return;

        PlasticInteractable plastic = target.GetComponent<PlasticInteractable>();
        if (plastic != null)
        {
            AddToInventory(plastic);
        }
    }

    private void AddToInventory(PlasticInteractable interactableObj)
    {
        if (interactableObj != null)
            Inventory.AddToEmptySlot(interactableObj);

        // Remove the reference to the previous obejct
        target = null;
    }
}
