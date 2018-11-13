using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Action : MonoBehaviour
{
    public KeyCode Key;

    protected GameObject target;

    public abstract void Act();

    private void Update()
    {
        if (Input.GetKeyDown(Key))
            Act();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        target = collision.gameObject;
        //Debug.Log("Target: " + target);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        target = null;
    }
}
