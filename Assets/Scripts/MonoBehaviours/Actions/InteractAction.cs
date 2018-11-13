using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractAction : Action
{
    public UnityEvent OnTurtlePickup = new UnityEvent();

    public override void Act()
    {
        if (target == null)
            return;

        TurtleInteractable turtle = target.GetComponent<TurtleInteractable>();
        if (turtle != null)
        {
            turtle.Save();
            OnTurtlePickup.Invoke();
        }
    }
}
