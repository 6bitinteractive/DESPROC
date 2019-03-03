using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnInteractObjective(GameObject target);

public class InteractObjective : Action
{
    public event OnInteractObjective onInteractObjective;
    public override void Act()
    {
        if (target == null)
            return;

        ObjectiveInteractable objective = target.GetComponent<ObjectiveInteractable>();
        {
            if (objective != null && !objective.isCollected)
            {
                objective.isCollected = true;
                onInteractObjective.Invoke(objective.gameObject);
            }
        }
    }
}

