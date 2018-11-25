using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Action : MonoBehaviour
{
    [SerializeField] protected KeyCode Key;
    [SerializeField] protected LayerMask interactableLayerMask;

    protected GameObject target;

    public abstract void Act();

    protected virtual void Start()
    { }

    protected virtual void Update()
    {
        //Debug.Log("Target: " + target);
        if (Input.GetKeyDown(Key))
        {
            Act();
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if ((interactableLayerMask.value & 1 << collision.gameObject.layer) != 0)
        {
            target = collision.gameObject;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        target = null;
    }
}

// Reference: Check layer mask for collision
// http://answers.unity.com/answers/454913/view.html
// https://gamedev.stackexchange.com/questions/119667/how-to-get-the-gameobjects-layermask
