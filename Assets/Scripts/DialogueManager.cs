using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public GameEvent OnDialogueEnd;
    //private Queue<string> sentences;
    private Queue<Dialogue> dialogue;

    void Awake ()
    {
        //sentences = new Queue<string>();
        dialogue = new Queue<Dialogue>();
    }

    public void StartDialogue(Dialogue[] dialogueArray)
    {
        animator.SetBool("IsOpen", true);
        //nameText.text = dialogue.name;

        // Clear previous sentences
        //sentences.Clear();
        dialogue.Clear();

        //foreach(string sentence in dialogue.sentences)
        //{
        //    sentences.Enqueue(sentence);
        //}
        foreach(Dialogue dialogueEntry in dialogueArray)
        {
            dialogue.Enqueue(dialogueEntry);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // If no sentences remain, end dialogue
        //if(sentences.Count <= 0)
        //{
        //    EndDialogue();
        //    return;
        //}
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
        OnDialogueEnd.Raise();
        Debug.Log("Raised onDialogueEnd");
    }
}
