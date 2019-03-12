using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    [SerializeField] private GameObject questPrefab;
    [SerializeField] private Transform questParent;
    [SerializeField] private Text questDescriptionText;
    [SerializeField] private InteractObjective interactObjective;
    public TurtleTale.SessionData sessionData;

    private Quests selected;
    private List<QuestScript> questScripts = new List<QuestScript>();

    private static QuestLog instance;

    public static QuestLog Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<QuestLog>();
            }
            return instance;
        }
    }

    public void Awake()
    {
        // Create quests for persistence
        if (sessionData.Quests != null)
        {
            foreach (Quests quest in sessionData.Quests)
            {
                CreateQuest(quest);
            }
        }     
    }

    public void AcceptQuest(Quests quest)
    {
        foreach (CollectObjective objectives in quest.CollectObjectives)
        {
            interactObjective.onInteractObjective += new OnInteractObjective(objectives.UpdateItemCount);

            quest.IsAccepted = true;
            quest.OnQuestAccepted.Invoke();
           // objectives.CheckItemCount(); used for checking in inventory fix later
        }

        sessionData.Quests.Add(quest);
        CreateQuest(quest);
        CheckCompletion(); //Check if already completed before accepting quest
    }

    public void DisplayDescription(Quests quest)
    {
        if (quest != null)
        {
            if (selected != null)
            {
                selected.QuestScript.Deselect();
            }

            string objectives = string.Format("<size=100>\nObjectives\n</size>");

            selected = quest;

            // Add objectives
            foreach (Objective obj in quest.CollectObjectives)
            {
                objectives += quest.Objective + ": " + obj.CurrentAmount + "/" + obj.Amount + "\n";
                // objectives += obj.Type + ": " + obj.CurrentAmount + "/" + obj.Amount + "\n";
            }

            questDescriptionText.text = string.Format("<size=100>{0}</size>\n\n{1}\n{2}", quest.Name, quest.Description, objectives);
        }
    }

    public void UpdateSelectedQuest()
    {
        DisplayDescription(selected);
    }

    public void CheckCompletion()
    {
        foreach (QuestScript questScript in questScripts)
        {
            questScript.Quest.QuestGiver.UpdateQuestStatus();
            questScript.IsComplete();
        }
    }

    public bool HasQuest(Quests quest)
    {
        // Check if quest has the same name
        return sessionData.Quests.Exists(x => x.Name == quest.Name);
    }

    public void RemoveQuest(QuestScript questScript)
    {
        if (questScript !=  null)
        {
            questScripts.Remove(questScript);
            Destroy(questScript.gameObject);
            sessionData.Quests.Remove(questScript.Quest);
            questDescriptionText.text = string.Empty;
            selected = null;
            questScript.Quest.QuestGiver.UpdateQuestStatus();
            questScript = null;
        }
      

    }

    public void CreateQuest(Quests quest)
    {
        GameObject gameObject = Instantiate(questPrefab, questParent);
        QuestScript questScript = gameObject.GetComponent<QuestScript>();

        // Assign reference to questscript
        quest.QuestScript = questScript;
        questScript.Quest = quest;
        questScripts.Add(questScript);

        gameObject.GetComponent<Text>().text = quest.Name;
    }
}
