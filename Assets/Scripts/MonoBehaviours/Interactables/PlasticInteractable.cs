using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlasticInteractable : Interactable
{
    public UnityEvent OnPlasticPickup = new UnityEvent();

    public Plastic GetPlasticData()
    {
        return base.GetInteractable() as Plastic;
    }

    public void Pickup()
    {
        OnPlasticPickup.Invoke();
        //Destroy(gameObject);
        gameObject.SetActive(false);

    }
}
