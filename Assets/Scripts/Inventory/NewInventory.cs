using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//[System.Serializable] public class OnAddedToInventory : UnityEvent<Interactable> { };

public class NewInventory : MonoBehaviour
{
    public OnAddedToInventory OnAddedToInventory = new OnAddedToInventory();
    public UnityEvent OnEmptyInventory = new UnityEvent();
    public UnityEvent OnInventoryFull = new UnityEvent();

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

    public void AddToInventory(PlasticInteractable item)
    {
        //Checks if item can be placed in stack
        if (item.GetStackSize() > 0)
        {
            if (IsStackable(item))
            {
                return;
            }
        }

        // Else place in a new empty slot
        IsNotStackable(item);
    }

    private bool IsStackable(PlasticInteractable item)
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

    private bool IsNotStackable(PlasticInteractable item)
    {
        foreach (Slot slot in InventorySlots)
        {
            //Checks if slot is empty
            if (slot.IsEmpty)
            {
                slot.AddItem(Instantiate(item)); // Add item to slot
                OnAddedToInventory.Invoke(item);
                return true;
            }
        }

        // Inventory is full cannot add anymore items
        OnInventoryFull.Invoke();
        Debug.Log("Inventory is full.");
        return false;
        
    }

    public void AddItem(PlasticInteractable item)
    {
        AddToInventory(item);
    }

    public void Empty()
    {
        /*
        // Move inventory data to bin data
        sessionData.Bin.AddRange(sessionData.Inventory);

        // Empty inventory data
        sessionData.Inventory.Clear();

        for (int i = 0; i < slotCount; i++)
        {
            if (inventorySlots[i].inventoryItem == null)
                break;

            inventorySlots[i].inventoryItem.gameObject.SetActive(false);
            inventorySlots[i].inventoryItem = null;
        }

        currentEmptySlot = 0;
        OnEmptyInventory.Invoke();
        */
    }

}
