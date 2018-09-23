using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Action : MonoBehaviour
{
    public KeyCode Key;
    protected GameObject target;

    private void Update()
    {
        if (Input.GetKeyDown(Key))
            Act();
    }

    public abstract void Act();
}
