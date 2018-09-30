using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]

public class TurtleInteractable : Interactable
{
    public UnityEvent OnTurtleSave = new UnityEvent();

    public void Save()
    {
        OnTurtleSave.Invoke();
        gameObject.SetActive(false);
    }
}
