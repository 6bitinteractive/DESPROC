using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryCapacityProgressBar : ProgressBar
{
    protected override void SetProgressBarText(float percentage)
    {
        ProgressBarText.text = string.Format("Inventory Capacity: {0}%", Mathf.RoundToInt(percentage * 100f));
    }

    protected override void InitializeData()
    {
        total = playerDataHandler.playerData.InventoryCapacity;

        int inventoryCount = playerDataHandler.playerData.Inventory.Count;
        current = (inventoryCount > 0) ? inventoryCount : 0;
    }
}
