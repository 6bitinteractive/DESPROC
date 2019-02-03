using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        // Lol find object, please replace later on
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
