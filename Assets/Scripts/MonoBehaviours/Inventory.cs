using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public UnityEvent OnAddedToInventory = new UnityEvent();
    public UnityEvent OnEmptyInventory = new UnityEvent();
    public UnityEvent OnInventoryFull = new UnityEvent();

    [Header("Inventory Slots")]
    [SerializeField] private GameObject gridContainer;
    [SerializeField] private GameObject inventorySlotPrefab;
    [SerializeField] private int slotCount = 9;

    private InventorySlot[] inventorySlots;
    private int currentEmptySlot = 0;

    void Start()
    {
        inventorySlots = new InventorySlot[slotCount];

        for (int i = 0; i < slotCount; i++)
        {
            // Create a slot
            GameObject slot = Instantiate(inventorySlotPrefab);
            slot.transform.SetParent(gridContainer.transform, false);

            // Add the InventorySlot component to the array
            inventorySlots[i] = slot.GetComponent<InventorySlot>();
        }
    }

    public void AddToEmptySlot(PlasticInteractable interactableObj)
    {
        if (inventorySlots[currentEmptySlot].inventoryItem == null)
        {
            inventorySlots[currentEmptySlot].inventoryItem = interactableObj;
            inventorySlots[currentEmptySlot].UpdateImage();
            interactableObj.transform.position = inventorySlots[currentEmptySlot].transform.position;
            interactableObj.transform.SetParent(inventorySlots[currentEmptySlot].transform);
            interactableObj.GetComponent<CircleCollider2D>().enabled = false; // Make sure it's not interactable once it's in the inventory

            // Move to next empty slot
            currentEmptySlot++;
            currentEmptySlot %= slotCount; // Avoid index going out of range

            // Broadcast that trash has been added to the inventory
            OnAddedToInventory.Invoke();
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
        OnEmptyInventory.Invoke();
    }
}
