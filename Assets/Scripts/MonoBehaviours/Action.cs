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

    private void Update()
    {
        //Debug.Log("Target: " + target);
        if (Input.GetKeyDown(Key))
        {
            Act();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((interactableLayerMask.value & 1 << collision.gameObject.layer) != 0)
        {
            target = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        target = null;
    }
}

// Reference: Check layer mask for collision
// http://answers.unity.com/answers/454913/view.html
