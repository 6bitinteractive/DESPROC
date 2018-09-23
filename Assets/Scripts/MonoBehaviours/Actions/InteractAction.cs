using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractAction : Action
{
    public override void Act()
    {
        if (target.GetComponent<TurtleInteractable>() != null)
            Debug.Log("Hello, turtle-san.");
    }
}
