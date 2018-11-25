using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class OnAddedToInventory : UnityEvent<Interactable> { };

public class Inventory : MonoBehaviour
{
    //public UnityEvent OnAddedToInventory = new UnityEvent();
    public OnAddedToInventory OnAddedToInventory = new OnAddedToInventory();
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
            // Store to inventory slot
            inventorySlots[currentEmptySlot].inventoryItem = interactableObj;

            // Update the slot image
            inventorySlots[currentEmptySlot].UpdateImage();

            // Deactivate interactableObject in scene
            interactableObj.gameObject.SetActive(false);

            // Move to next empty slot
            currentEmptySlot++;
            currentEmptySlot %= slotCount; // Avoid index going out of range

            // Broadcast that trash has been added to the inventory
            OnAddedToInventory.Invoke(interactableObj);
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

    public int GetSlotCount()
    {
        return slotCount;
    }
}
