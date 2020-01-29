using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Player player;
    public Animator animator;

    private Color keywordColor = Color.cyan;
    private Queue<Sentence> sentences;
    private Dialogue[] triggerArray;
    private Sentence[] toDisplay;
    private GameEvent endTrigger;
    private bool coroutineRunning;

    public UnityEvent OnFullDialogueEnd; // Note: Added this to avoid creating different GameEvent triggers

    void Awake()
    {
        sentences = new Queue<Sentence>();
        if (OnFullDialogueEnd == null) OnFullDialogueEnd = new UnityEvent();
    }

    public void StartDialogue(DialogueTrigger dialogueTrigger)
    {
        // Clear cached toDisplay variable
        toDisplay = null;

        if ((animator != null) && (animator.isActiveAndEnabled))
        {
            animator.SetBool("IsOpen", true);
        }

        if (player != null && player.gameObject.layer == 8)
        {
            if (player.GetComponent<Movement>() != null)
                player.GetComponent<Movement>().DisableMovement();

            if (player.GetComponent<PlayerMobileController>() != null)
                player.GetComponent<PlayerMobileController>().SetIsMoving(false);
        }

        triggerArray = dialogueTrigger.dialogueArray;

        // Clear previous sentences
        sentences.Clear();

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
        AdvanceDialogue();
    }

    public Sentence[] DetermineDisplay(DialogueTrigger dialogueTrigger)
    {
        for (int i = 0; i < triggerArray.Length; i++)
        {
            if (QuestLog.Instance != null)
            {
                // If quest log is empty
                if ((QuestLog.Instance.sessionData.Quests == null))
                {
                    //Debug.Log("1");
                }
                // If dialogue trigger quest name is empty or doesn't match any quest on the quest log
                else if ((QuestLog.Instance.sessionData.Quests.Exists(x => x.Name == triggerArray[i].questName) == false))
                {
                    //Debug.Log("2");
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

    public void DisplayNextSentence(Sentence sentenceEntry)
    {
        // Makes sure all sentence animations are stopped before typing in new sentence
        StopAllCoroutines();
        StartCoroutine(StartTyping(sentenceEntry));
    }

    void SkipTextTyping()
    {
        // Stop sentence animation to display full text, then dequeue current sentence
        StopAllCoroutines();
        coroutineRunning = false;

        Sentence sentenceEntry = sentences.Peek();
        if (sentenceEntry.sentence.Contains("<keyword>"))
        {
            string stringBeforeTag = sentenceEntry.sentence.Substring(0, sentenceEntry.sentence.IndexOf("<keyword>"));
            string stringAfterTag = sentenceEntry.sentence.Substring(sentenceEntry.sentence.LastIndexOf("</keyword>") + 10);

            dialogueText.text = stringBeforeTag +
                "<color=" + ColorToHexString(keywordColor) + ">" + ExtractKeyword(sentenceEntry.sentence, "keyword") +
                "</color>" + stringAfterTag;
        }
        else
        {
            dialogueText.text = sentences.Peek().sentence.ToString();
        }
        sentences.Dequeue();
    }

    public void AdvanceDialogue()
    {
        // If no dialogue entries remain, end dialogue
        if (sentences.Count <= 0)
        {
            EndDialogue();
            return;
        }

        // If text is currently being typed
        if (coroutineRunning)
        {
            SkipTextTyping();
        }
        // If no coroutine is running, type next sentence
        else
        {
            DisplayNextSentence(sentences.Peek());
        }
    }

    IEnumerator StartTyping(Sentence sentenceEntry)
    {
        // Only dequeue sentence after coroutine has finished, so SkipTextTyping() can display the current sentence
        yield return TypeSentence(sentenceEntry);
        // sentences.Dequeue(); // This causes a bug in which names can interchange when talking to other people from what I see it causes the TypeSentence() function to not occur consistently
    }

    // Types sentence per letter
    IEnumerator TypeSentence(Sentence sentenceEntry)
    {
        coroutineRunning = true;
        yield return null;
        nameText.text = sentenceEntry.name;
        dialogueText.text = "";

        if (sentenceEntry.sentence.Contains("<keyword>"))
        {
            string stringBeforeTag = sentenceEntry.sentence.Substring(0, sentenceEntry.sentence.IndexOf("<keyword>"));
            string stringAfterTag = sentenceEntry.sentence.Substring(sentenceEntry.sentence.LastIndexOf("</keyword>") + 10);

            foreach (char letter in stringBeforeTag.ToCharArray())
            {
                dialogueText.text += letter;
                yield return null;
            }
            foreach (char letter in ExtractKeyword(sentenceEntry.sentence, "keyword"))
            {
                dialogueText.text += "<color=" + ColorToHexString(keywordColor) + ">" + letter + "</color>";
                yield return null;
            }
            foreach (char letter in stringAfterTag.ToCharArray())
            {
                dialogueText.text += letter;
                yield return null;
            }
        }
        else
        {
            foreach (char letter in sentenceEntry.sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return null;
            }
        }
        coroutineRunning = false;
        sentences.Dequeue();
    }

    string ExtractKeyword(string s, string tag)
    {
        string startTag = "<" + tag + ">";
        int startIndex = s.IndexOf(startTag) + startTag.Length;
        int endIndex = s.IndexOf("</" + tag + ">", startIndex);
        return s.Substring(startIndex, endIndex - startIndex);
    }

    string ColorToHexString(Color color)
    {
        Color32 color32 = color;
        return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", color32.r, color32.g, color32.b, color32.a);
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
        StartCoroutine(EndingDialogue());
        /*
        OnFullDialogueEnd.Invoke();

        if (endTrigger != null)
        {
            endTrigger.Raise();
        }

        if ((animator != null) && (animator.isActiveAndEnabled))
        {
            animator.SetBool("IsOpen", false);
        }

        if (player != null && player.gameObject.layer == 8)
        {
            player.GetComponent<Movement>().EnableMovement();
        }
        */
    }

    private IEnumerator EndingDialogue()
    {
        yield return new WaitForSeconds(0.5f);
        OnFullDialogueEnd.Invoke();

        if (endTrigger != null)
        {
            endTrigger.Raise();
        }

        if ((animator != null) && (animator.isActiveAndEnabled))
        {
            animator.SetBool("IsOpen", false);
        }

        if (player != null && player.gameObject.layer == 8)
        {
            player.GetComponent<Movement>().EnableMovement();
        }
    }
}
