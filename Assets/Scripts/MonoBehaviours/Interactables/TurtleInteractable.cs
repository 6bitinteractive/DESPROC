using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]

public class TurtleInteractable : Interactable
{
    public override void Interact()
    {
        OnInteract.Invoke();
        gameObject.SetActive(false);
    }
}
