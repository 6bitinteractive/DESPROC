using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryCapacityProgressBar : ProgressBar
{
    protected override void Start()
    {
        total = 9; // Fix: hard-code

        base.Start();
    }

    protected override void SetProgressBarText(float percentage)
    {
        ProgressBarText.text = string.Format("Inventory Capacity: {0}%", Mathf.RoundToInt(percentage * 100f));
    }
}
