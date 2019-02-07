using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewInventory : MonoBehaviour
{
    public GameObject SlotPrefab;
    public int SlotLimit;

    public List<Slot> InventorySlots = new List<Slot>();

    // Use this for initialization
    void Start ()
    {
        // Add slots
        for (int i = 0; i < SlotLimit; i++)
        {
            Slot slot = Instantiate(SlotPrefab, transform).GetComponent<Slot>();
            InventorySlots.Add(slot);
        }	
	}

    public void AddToInventory(ItemBase item)
    {
        //Checks if item can be placed in stack
        if (item.ItemStackSize > 0)
        {
            if (IsStackable(item))
            {
                return;
            }
        }

        // Else place in a new empty slot
        IsNotStackable(item);
    }

    private bool IsStackable(ItemBase item)
    {
        foreach (Slot slot in InventorySlots)
        {
            if (slot.StackItem(item))
            {
                return true;
            }
        }
        return false;
    }

    private bool IsNotStackable(ItemBase item)
    {
        foreach (Slot slot in InventorySlots)
        {
            //Checks if slot is empty
            if (slot.IsEmpty)
            {
                slot.AddItem(Instantiate(item)); // Add item to slot

                return true;
            }
        }

        // Inventory is full cannot add anymore items
        return false;
        
    }

    public void AddItem(ItemBase item)
    {
        AddToInventory(item);
    }
    
}
