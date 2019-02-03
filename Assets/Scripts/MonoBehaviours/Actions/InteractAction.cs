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

        RequirementInteractable requirement = target.GetComponent<RequirementInteractable>();
        {
            if (requirement != null)
            {
                requirement.Pickup();
            }
        }

        /*
        TurtleInteractable turtle = target.GetComponent<TurtleInteractable>();
        if (turtle != null)
        {
            turtle.Save();
            OnTurtlePickup.Invoke();
        }
        */
    }
}
