using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
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

            // Move to next empty slot
            currentEmptySlot++;
            currentEmptySlot %= slotCount; // Avoid index going out of range
        }
    }
}
