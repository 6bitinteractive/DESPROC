﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AddToInventoryAction : InventoryAction
{
    [Header("Temporary Hacks")]
    [SerializeField] private TurtleTale.SessionData sessionData;
    //[SerializeField] private PlayerDataHandler playerDataHandler;
    [SerializeField] private PlasticInteractable plasticPrefab;

    protected override void Start()
    {
        base.Start();

        //// We add whatever trash is in the player data to the inventory, else there's nothing to do
        //int inventoryCountInData = playerDataHandler.playerData.Inventory.Count;

        //if (inventoryCountInData <= 0 || inventoryCountInData > Inventory.GetSlotCount())
        //    return;

        //for (int i = 0; i < inventoryCountInData; i++)
        //{
        //    PlasticInteractable p = Instantiate(plasticPrefab) as PlasticInteractable;
        //    PlasticData plasticInInventory = playerDataHandler.playerData.Inventory[i];

        //    for (int j = 0; j < p.GetFactory().BaseObjects.Length; j++)
        //    {
        //        if (plasticInInventory == p.GetFactory().BaseObjects[j])
        //        {
        //            p.baseIndex = j;
        //        }
        //    }

        //    Debug.Log(p);
        //    AddToInventory(p, true);
        //}

        // We add whatever trash is in the player data to the inventory, else there's nothing to do
        //int inventoryCountInData = sessionData.Inventory.Count;

        //if (inventoryCountInData <= 0 || inventoryCountInData > Inventory.GetSlotCount())
        //    return;

        //for (int i = 0; i < inventoryCountInData; i++)
        //{
        //    PlasticInteractable p = Instantiate(plasticPrefab) as PlasticInteractable;
        //    PlasticData plasticInInventory = sessionData.Inventory[i];

        //    for (int j = 0; j < p.GetFactory().BaseObjects.Length; j++)
        //    {
        //        if (plasticInInventory == p.GetFactory().BaseObjects[j])
        //        {
        //            p.baseIndex = j;
        //        }
        //    }

        //    Debug.Log(p.name);
        //    AddToInventory(p, true);
        //}
    }

    public override void Act()
    {
        if (target == null)
            return;

        PlasticInteractable plastic = target.GetComponent<PlasticInteractable>();
        if (plastic != null)
        {
            AddToInventory(plastic);
        }
    }

    protected void AddToInventory(PlasticInteractable interactableObj, bool alreadyInPlayerData = false)
    {
        //if (playerDataHandler.playerData.Inventory.Count == Inventory.GetSlotCount())
        //{
        //    Inventory.OnInventoryFull.Invoke();
        //    return;
        //}

        //if (interactableObj != null)
        //    Inventory.AddToEmptySlot(interactableObj);

        //if (!alreadyInPlayerData)
        //{
        //    // Add the plastic scriptable object to player data list
        //    playerDataHandler.playerData.Inventory.Add(interactableObj.GetPlastic());

        //    // Increase total plastic collected in player data
        //    playerDataHandler.playerData.TotalTrash++;
        //}


        /* Old Working Method
        if (sessionData.Inventory.Count == Inventory.GetSlotCount())
        {
            Inventory.OnInventoryFull.Invoke();
            return;
        }

        if (interactableObj != null)
            Inventory.AddToEmptySlot(interactableObj, alreadyInPlayerData);


        // Remove the reference to the previous obejct
        target = null;
        */

        Inventory.AddItem(interactableObj);
        interactableObj.GetComponent<SpriteRenderer>().enabled = false;  // Hides this sprite
        Destroy(interactableObj);
        target = null;
    }
}
