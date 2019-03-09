using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    public UnityEvent OnInteract = new UnityEvent();

    public abstract void Interact();
}
