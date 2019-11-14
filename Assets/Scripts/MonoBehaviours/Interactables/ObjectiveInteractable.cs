using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]

public class ObjectiveInteractable : MonoBehaviour
{
    public string Name;
    public bool isCollected;
    public UnityEvent OnObjectiveInteract = new UnityEvent();

    public void InteractObjective()
    {
        OnObjectiveInteract.Invoke();
    }
}
