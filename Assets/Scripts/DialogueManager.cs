using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Player player;
    public Animator animator;
    private Queue<Dialogue> dialogue;
    private GameEvent trigger;
 
    void Awake ()
    {
        dialogue = new Queue<Dialogue>();
    }

    public void StartDialogue(DialogueTrigger dialogueTrigger)
    {
        animator.SetBool("IsOpen", true);
        trigger = dialogueTrigger.dialogueEndTrigger;

        if (player.gameObject.layer == 8)
        {
            player.GetComponent<Movement>().xSpeed = 0;
            player.GetComponent<Movement>().ySpeed = 0;
        }

        // Clear previous sentences
        dialogue.Clear();

        foreach(Dialogue dialogueEntry in dialogueTrigger.dialogueArray)
        {
            dialogue.Enqueue(dialogueEntry);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // If no dialogue entries remain, end dialogue
        if (dialogue.Count <= 0)
        {
            EndDialogue();
            return;
        }

        Dialogue dialogueEntry = dialogue.Dequeue();
        // Makes sure all sentence animations are stopped before typing in new sentence
        StopAllCoroutines();
        StartCoroutine(TypeSentence(dialogueEntry));
    }

    // Types sentence per letter
    IEnumerator TypeSentence(Dialogue dialogueEntry)
    {
        nameText.text = dialogueEntry.name;
        dialogueText.text = "";

        foreach (char letter in dialogueEntry.sentence.ToCharArray())
        {
            // Adds letter to dialogue text string every after 1 frame
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        if (player.gameObject.layer == 8)
        {
            player.GetComponent<Movement>().xSpeed = 2.5f;
            player.GetComponent<Movement>().ySpeed = 2.5f;
        }
        if (trigger!= null)
        {
            trigger.Raise();
        }
    }
}
