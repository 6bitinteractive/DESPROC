using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public UnityEvent OnInventoryFull = new UnityEvent();

    [SerializeField] private GameObject gridContainer;
    private InventorySlot[] inventorySlots;
    private int currentEmptySlot = 0;
    private int slotCount;

    void Start()
    {
        slotCount = gridContainer.transform.childCount;
        inventorySlots = new InventorySlot[slotCount];

        for (int i = 0; i < slotCount; i++)
        {
            inventorySlots[i] = gridContainer.transform.GetChild(i).GetComponent<InventorySlot>();
        }
    }

    public void AddToEmptySlot(InventoryInteractable interactableObj)
    {
        if (inventorySlots[currentEmptySlot].inventoryItem == null)
        {
            inventorySlots[currentEmptySlot].inventoryItem = interactableObj;
            interactableObj.transform.position = inventorySlots[currentEmptySlot].transform.position;
            interactableObj.transform.SetParent(inventorySlots[currentEmptySlot].transform);
            interactableObj.GetComponent<CircleCollider2D>().enabled = false; // Make sure it's not interactable once it's in the inventory

            // Move to next empty slot
            currentEmptySlot++;
            currentEmptySlot %= slotCount; // Avoid index going out of range
        }
        else
        {
            OnInventoryFull.Invoke();
            Debug.Log("Inventory is full.");
        }
    }

    public void Empty()
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (inventorySlots[i].inventoryItem == null)
                break;

            inventorySlots[i].inventoryItem.gameObject.SetActive(false);
            inventorySlots[i].inventoryItem = null;
        }

        currentEmptySlot = 0;
    }
}
