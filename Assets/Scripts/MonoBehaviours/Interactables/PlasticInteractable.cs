using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticInteractable : Interactable
{
    public Plastic GetPlastic()
    {
        return base.GetInteractable() as Plastic;
    }
}
