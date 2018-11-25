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

    [Header("Temporary Hacks")]
    [SerializeField] private PlayerDataHandler playerDataHandler;
    [SerializeField] private PlasticInteractable plasticPrefab;

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

        // We add whatever trash is in the player data to the inventory UI, else there's nothing to do
        int inventoryCount = playerDataHandler.playerData.Inventory.Count;

        if (inventoryCount <= 0 || inventoryCount >= slotCount)
            return;

        for (int i = 0; i < inventoryCount; i++)
        {
            PlasticInteractable p = Instantiate(plasticPrefab) as PlasticInteractable;
            Plastic plasticInInventory = playerDataHandler.playerData.Inventory[i];

            for (int j = 0; j < p.GetFactory().BaseObjects.Length; j++)
            {
                if (plasticInInventory == p.GetFactory().BaseObjects[j])
                {
                    p.baseIndex = j;
                    break;
                }

            }

            AddToEmptySlot(p, true);
        }
    }

    public void AddToEmptySlot(PlasticInteractable interactableObj, bool alreadyInPlayerData = false)
    {
        if (inventorySlots[currentEmptySlot].inventoryItem == null)
        {
            // Store to inventory slot
            inventorySlots[currentEmptySlot].inventoryItem = interactableObj;

            // Update the slot image
            inventorySlots[currentEmptySlot].UpdateImage();

            // Add the plastic scriptable object to player data list
            if (!alreadyInPlayerData)
                playerDataHandler.playerData.Inventory.Add(interactableObj.GetPlastic());

            // Increase total plastic collected in player data
            playerDataHandler.playerData.TotalTrash++;

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
}
