using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue[] dialogueArray;
    public GameEvent dialogueEndTrigger;

    public void TriggerDialogue()
    {
        // Uses FindObjectOfType, find alternative methods for this later on
        FindObjectOfType<DialogueManager>().StartDialogue(this);
    }
}
