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
    private Dialogue[] toDisplay;
 
    void Awake ()
    {
        dialogue = new Queue<Dialogue>();
    }

    public void StartDialogue(DialogueTrigger dialogueTrigger)
    {
        if (animator != null)
        {
            animator.SetBool("IsOpen", true);
        }
           
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

        // If quest log is empty or dialogue trigger doesn't contain a quest name, display default dialogue
        if ((dialogueTrigger.questName == null) || (QuestLog.Instance == null))
        {
            toDisplay = dialogueTrigger.dialogueArray;
        }
        else
        {
            // If dialogue trigger quest name matches a quest on the quest list
            if (QuestLog.Instance.sessionData.Quests.Exists(x => x.Name == dialogueTrigger.questName))
            {
                // Set quest dialogue array for display
                toDisplay = dialogueTrigger.questDialogueArray;
            }
        }

        if(toDisplay != null)
        {
            foreach (Dialogue dialogueEntry in toDisplay)
            {
                dialogue.Enqueue(dialogueEntry);
            }
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
        if (animator != null)
        {
            animator.SetBool("IsOpen", false);
        }

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
