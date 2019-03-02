using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    [SerializeField] private GameObject questPrefab;
    [SerializeField] private Transform questParent;
    [SerializeField] private Text questDescriptionText;
    [SerializeField] private InteractAction action;

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

    public void AcceptQuest(Quests quest)
    {
        foreach (CollectObjective objectives in quest.CollectObjectives)
        {
            action.onInteractAction += new OnInteractAction(objectives.UpdateItemCount);
        }

        GameObject gameObject = Instantiate(questPrefab, questParent);
        QuestScript questScript = gameObject.GetComponent<QuestScript>();

        // Assign reference to questscript
        quest.QuestScript = questScript;
        questScript.Quest = quest;
        questScripts.Add(questScript);

        
        gameObject.GetComponent<Text>().text = quest.Name;

    }

    public void DisplayDescription(Quests quest)
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
            objectives += obj.Type + ": " + obj.CurrentAmount + "/" + obj.Amount + "\n";
        }

        questDescriptionText.text = string.Format("<size=100>{0}</size>\n{1}\n{2}", quest.Name, quest.Description, objectives);
    }

    public void UpdateSelectedQuest()
    {
        DisplayDescription(selected);
    }

    public void CheckCompletion()
    {
        foreach (QuestScript questScript in questScripts)
        {
            questScript.IsComplete();
        }
    }
}
