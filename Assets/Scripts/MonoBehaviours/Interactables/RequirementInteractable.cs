using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]

public class RequirementInteractable : Interactable
{
    public UnityEvent OnRequirementPickup = new UnityEvent();

    public void Pickup()
    {
        OnRequirementPickup.Invoke();
        gameObject.SetActive(false);
    }
}
