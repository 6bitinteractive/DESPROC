using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ItemBase : MonoBehaviour, IUseable
{
    public string ItemName;
    public Sprite ItemIcon;
    public int ItemStackSize;
    public Slot ItemSlot;

    public UnityEvent OnItemUse = new UnityEvent();

    public void RemoveItem()
    {
        if (ItemSlot != null)
        {
           // Debug.Log("Item Removed");
          //  ItemSlot.RemoveItem(this);
        }
    }
   
    public void test()
    {
        Debug.Log(ItemName);
    }
 
    public virtual void OnUse()
    {
       
    }

    public void Use()
    {
       OnItemUse.Invoke();
    }
}
