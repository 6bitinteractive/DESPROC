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

    private Queue<Sentence> sentences;
    private Dialogue[] triggerArray;
    private Sentence[] toDisplay;
    private GameEvent endTrigger;

    void Awake ()
    {
        sentences = new Queue<Sentence>();
    }

    public void StartDialogue(DialogueTrigger dialogueTrigger)
    {
        // Clear cached toDisplay variable
        toDisplay = null;

        if ((animator != null) &&  (animator.isActiveAndEnabled))
        {
            animator.SetBool("IsOpen", true);
        }

        if(player != null)
        {
            if (player.gameObject.layer == 8)
            {
                player.GetComponent<Animator>().SetBool("isMoving", false);
                player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                player.GetComponent<Movement>().enabled = false;
            }
        }

        triggerArray = dialogueTrigger.dialogueArray;

        // Clear previous sentences
        sentences.Clear();

        //for (int i = 0; i < triggerArray.Length; i++)
        //{
        //    // If dialogue quest name matches a quest on the quest list
        //    if (QuestLog.Instance.sessionData.Quests.Exists(x => x.Name == triggerArray[i].questName))
        //    {
        //        for (int j = 0; j < QuestLog.Instance.sessionData.Quests.Count; j++)
        //        {
        //            // If quest exists and is not complete
        //            if ((QuestLog.Instance.sessionData.Quests[j].Name == triggerArray[i].questName) && (QuestLog.Instance.sessionData.Quests[j].IsComplete == false))
        //            {
        //                // Set quest dialogue array for display
        //                Debug.Log("Quest sentence displayed.");
        //                toDisplay = triggerArray[i].sentenceArray;
        //                endTrigger = triggerArray[i].dialogueEndTrigger;
        //            }
        //        }
        //    }
        //    // If quest log is populated, but dialogue doesn't contain a quest
        //    else if ((QuestLog.Instance.sessionData.Quests.Exists(x => x.Name == triggerArray[i].questName) == false))
        //    {
        //        Debug.Log("Default sentence displayed.");
        //        toDisplay = triggerArray[0].sentenceArray;
        //        endTrigger = triggerArray[0].dialogueEndTrigger;
        //    }
        //}

        if (toDisplay == null)
        {
            toDisplay = DetermineDisplay(dialogueTrigger);
        }

        if (toDisplay != null)
        {
            foreach (Sentence sentenceEntry in toDisplay)
            {
                sentences.Enqueue(sentenceEntry);
            }
        }
        DisplayNextSentence();
    }

    public Sentence[] DetermineDisplay(DialogueTrigger dialogueTrigger)
    {
        for (int i = 0; i < triggerArray.Length; i++)
        {
            if(QuestLog.Instance != null)
            {
                // If quest log is empty
                if ((QuestLog.Instance.sessionData.Quests == null))
                {
                    Debug.Log("1");
                }
                // If dialogue trigger quest name is empty or doesn't match any quest on the quest log
                else if ((QuestLog.Instance.sessionData.Quests.Exists(x => x.Name == triggerArray[i].questName) == false))
                {
                    Debug.Log("2");
                }
                // If dialogue quest name matches a quest on the quest list
                else if (QuestLog.Instance.sessionData.Quests.Exists(x => x.Name == triggerArray[i].questName))
                {
                    for (int j = 0; j < QuestLog.Instance.sessionData.Quests.Count; j++)
                    {
                        // If quest exists and is not complete
                        if ((QuestLog.Instance.sessionData.Quests[j].Name == triggerArray[i].questName) && (QuestLog.Instance.sessionData.Quests[j].IsComplete == false))
                        {
                            // Set quest dialogue array for display
                            Debug.Log("Quest sentence displayed.");
                            endTrigger = triggerArray[i].dialogueEndTrigger;
                            return triggerArray[i].sentenceArray;
                        }
                    }
                }
            }
        }

        Debug.Log("Default sentence displayed.");
        endTrigger = triggerArray[0].dialogueEndTrigger;
        return triggerArray[0].sentenceArray;
    }

    public void DisplayNextSentence()
    {
        // If no dialogue entries remain, end dialogue
        if (sentences.Count <= 0)
        {
            EndDialogue();
            return;
        }

        Sentence sentenceEntry = sentences.Dequeue();
        // Makes sure all sentence animations are stopped before typing in new sentence
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentenceEntry));
    }

    // Types sentence per letter
    IEnumerator TypeSentence(Sentence sentenceEntry)
    {
        nameText.text = sentenceEntry.name;
        dialogueText.text = "";

        foreach (char letter in sentenceEntry.sentence.ToCharArray())
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

    public void RaiseEventTrigger(GameEvent eventTrigger)
    {
        if (eventTrigger != null)
        {
            eventTrigger.Raise();
        }
    }

    public void EndDialogue()
    {
        if (endTrigger != null)
        {
            endTrigger.Raise();
        }

        if ((animator != null) && (animator.isActiveAndEnabled))
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
    }
}
