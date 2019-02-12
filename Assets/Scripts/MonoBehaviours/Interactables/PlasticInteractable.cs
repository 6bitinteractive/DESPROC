using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlasticInteractable : Interactable
{
    public UnityEvent OnPlasticPickup = new UnityEvent();

    public Plastic GetPlastic()
    {
        return base.GetInteractable() as Plastic;
    }

    public override Sprite GetSprite()
    {
        return GetPlastic().Sprite;
    }

    public AudioClip GetPickupSfx()
    {
        return GetPlastic().PickupSfx;
    }

    public int GetStackSize()
    {
        return GetPlastic().StackSize;
    }

    public void Pickup()
    {
        OnPlasticPickup.Invoke();
        //Destroy(gameObject);
        gameObject.SetActive(false);

    }
}
