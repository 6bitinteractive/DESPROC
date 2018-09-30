using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractAction : Action
{
    public override void Act()
    {
        if (target == null)
            return;

        TurtleInteractable turtle = target.GetComponent<TurtleInteractable>();
        if (turtle != null)
        {
            turtle.Save();
        }

    }
}
