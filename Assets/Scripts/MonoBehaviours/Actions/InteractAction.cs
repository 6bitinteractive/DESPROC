using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public delegate void OnInteractAction(GameObject target);

public class InteractAction : Action
{
    public event OnInteractAction onInteractAction;
    public override void Act()
    {
        if (target == null)
            return;

        onInteractAction.Invoke(target);

        /*
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
