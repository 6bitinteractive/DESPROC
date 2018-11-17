using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyToBinAction : InventoryAction
{
    public override void Act()
    {
        if (target == null)
            return;

        if (target.GetComponent<Bin>() != null)
        {
            Inventory.Empty();
            Debug.Log("Empty inventory.");
        }
    }
}
