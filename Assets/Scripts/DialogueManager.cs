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

        if(player != null)
        {
            if (player.gameObject.layer == 8)
            {
                player.GetComponent<Animator>().SetBool("isMoving", false);
                player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                player.GetComponent<Movement>().enabled = false;
            }
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

    public void ChangeDialogueText(Text newText)
    {
        // Makes sure all sentence animations are stopped before changing dialogue text
        StopAllCoroutines();
        dialogueText = newText;
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);

        if (player != null)
        {
            if (player.gameObject.layer == 8)
            {
                player.GetComponent<Animator>().SetBool("isMoving", true);
                player.GetComponent<Movement>().enabled = true;
            }
        }

        if (trigger!= null)
        {
            trigger.Raise();
        }
    }
}
