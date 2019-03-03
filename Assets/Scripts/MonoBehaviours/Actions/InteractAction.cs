using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractAction : Action
{
    public override void Act()
    {
        if (target == null)
            return;
    }
}
