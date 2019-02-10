using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractNPC : Action
{
    public override void Act()
    {
        if (target == null)
            return;

        NPCInteractable npc = target.GetComponent<NPCInteractable>();
        {
            if (npc != null)
            {
                npc.Talk();
            }
        }
    }
}
