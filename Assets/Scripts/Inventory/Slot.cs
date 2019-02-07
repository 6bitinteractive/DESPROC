using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public Image SlotItemIcon;
    public Text StackSize;

    private List<PlasticInteractable> items = new List<PlasticInteractable>();

    public PlasticInteractable GetItem
    {
        get
        {
            // If there is an item on the slot
            if (!IsEmpty)
            {
                return items[0]; // return the item
            }

            return null;
        }
    }
    
    public bool IsEmpty
    {
        get
        {
            //Debug.Log("Empty");
            return items.Count == 0; //If stack is 0 then item slot is empty

        }
    }
    
    public void Start()
    {
        SlotItemIcon.color = Color.clear; // Set transparency to 0
        StackSize.color = Color.clear; // Set stack size color transparent
        StackSize.text = items.Count.ToString(); //Set stack size text to items count
    }

    public void RemoveItem(PlasticInteractable item)
    {
        //Checks if item count is 0
        if (!IsEmpty)
        {
            //Debug.Log("Deleted");
            items.RemoveAt(items.Count - 1);
            UpdateStackUI(); // Updates inventory UI      

            //If its empty destroy
            if (IsEmpty)
            {
                Destroy(item.gameObject);
            }
        }
    }

    public void UpdateStackUI()
    {
        if (IsEmpty)
        {
            SlotItemIcon.color = Color.clear; // Set color transparent
            StackSize.color = Color.clear; // Set stack size color transparent
        }
        else
        {
            StackSize.text = items.Count.ToString();

            Debug.Log(items.Count);

        }
    }

    public bool StackItem(PlasticInteractable item)
    {
        
        // Check if the item has the same name and if its count is less than stack size
        if (item.name == item.name && items.Count < item.GetStackSize() && !IsEmpty)
        {
            items.Add(item);
           // item.ItemSlot = this;
            UpdateStackUI();
            return true;
        }

        return false;
    }

    public bool AddItem(PlasticInteractable item)
    {

        items.Add(item); // Add item
        SlotItemIcon.sprite = item.GetSprite(); // Set slot item icon to new item icon
        SlotItemIcon.color = Color.white; // Reset transparency to default
       // item.ItemSlot = this; // Item reference to this slot
        StackSize.color = Color.black; // Set stack size color transparent
        UpdateStackUI(); // Update stack size text
        return true;
    }

    public void UseItem()
    {      
        // Check if the item has IUseable interface
        if (GetItem is IUseable)
        {
            (GetItem as IUseable).Use(); // Cast to useable then use Item
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       // ItemBase item = eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemBase>();

        // Check if left clicking
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            UseItem();
        }
    }
}
