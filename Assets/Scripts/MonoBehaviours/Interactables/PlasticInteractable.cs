using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticInteractable : Interactable
{
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
}
