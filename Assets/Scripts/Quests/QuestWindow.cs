using System.Collections;
using System.Collections.Generic;
using TurtleTale;
using UnityEngine;
using UnityEngine.UI;

public class QuestWindow : Window
{
    private QuestGiver questGiver;
    private Quests selectedQuest;
    [SerializeField] private GameObject questPrefab;
    [SerializeField] private Transform questArea;
    [SerializeField] private GameObject acceptButton;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject completeButton;
    [SerializeField] private GameObject questDescriptionText;
    [SerializeField] private List<GameObject> quests = new List<GameObject>();
    public SessionData sessionData;

    private static QuestWindow instance;

    public static QuestWindow Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<QuestWindow>();
            }
            return instance;
        }
    }

    public void DisplayQuests(QuestGiver questGiver)
    {
        this.questGiver = questGiver;

        if (!questGiver.IsDisplayingQuest)
        {        
            // Clear quests
            foreach (GameObject gameObject in quests)
            {
               Destroy(gameObject);
            }
            
            // Instantiate quests once 
            foreach (Quests quest in questGiver.questGiverData.QuestGiverQuests)
            {
                if (quest != null)
                {
                    GameObject gameObject = Instantiate(questPrefab, questArea);
                    gameObject.gameObject.GetComponentInChildren<Text>().text = quest.Name; // Display quest name
                    gameObject.gameObject.GetComponentInChildren<QuestGiverQuestScript>().Quest = quest; // Set quest
                    quests.Add(gameObject);
                    
               
                    for (int i = 0; i < QuestLog.Instance.sessionData.Quests.Count; i++)
                    {
                        // If quest is complete
                        if (QuestLog.Instance.HasQuest(quest) && QuestLog.Instance.sessionData.Quests[i].IsComplete)
                        {
                            gameObject.gameObject.GetComponentInChildren<Text>().color = Color.yellow;
                        }
                    }
                   
                    // If quest is accepted
                    if (QuestLog.Instance.HasQuest(quest))
                    {
                        Color color = gameObject.GetComponentInChildren<Text>().color;
                        color.a = 0.5f;
                        gameObject.GetComponentInChildren<Text>().color = color;
                    }
                }
            }
            questGiver.IsDisplayingQuest = true;
        }
    }

    public void DisplayQuestInfo(Quests quest)
    {
        if (quest != null)
        {
            this.selectedQuest = quest;

            for (int i = 0; i < QuestLog.Instance.sessionData.Quests.Count; i++)
            {
                //If the quest log already has the quest and its complete
                if (QuestLog.Instance.HasQuest(quest) && QuestLog.Instance.sessionData.Quests[i].IsComplete)
                {
                    acceptButton.SetActive(false); // Hide accept button
                    completeButton.SetActive(true); // Display complete button
                }
            }

            if (!QuestLog.Instance.HasQuest(quest))
            {
                acceptButton.SetActive(true); // Display accept button
            }

            else
            {
                acceptButton.SetActive(false);
            }
   
            questDescriptionText.SetActive(true); // Display description

            string objectives = string.Format("<size=100>\nObjectives\n</size>");

            // Add objectives 
            foreach (Objective obj in quest.CollectObjectives)
            {
                objectives += quest.Objective + ": " + obj.CurrentAmount + "/" + obj.Amount + "\n";
                // objectives += obj.Type + ": " + obj.CurrentAmount + "/" + obj.Amount + "\n";
            }

            questDescriptionText.GetComponent<Text>().text = string.Format("<size=100>{0}</size>\n\n{1}\n", quest.Name, quest.Description);
        }
    }

    public void UpdateQuestWindow()
    {
        DisplayQuests(questGiver);
        questGiver.IsDisplayingQuest = false;
    }

    public void Accept()
    {
        if (!selectedQuest.IsAccepted)
        {
            QuestLog.Instance.AcceptQuest(selectedQuest);
            UpdateQuestWindow();
            acceptButton.SetActive(false);
            transform.parent.parent.GetComponent<Movement>().EnableMovement();
        }
    }

    public void CompleteQuest()
    {
        if (selectedQuest.IsComplete)
        {
            for (int i = 0; i < questGiver.questGiverData.QuestGiverQuests.Count; i++)
            {
                // If the quest is the same as the selected
                if (selectedQuest == questGiver.questGiverData.QuestGiverQuests[i])
                {
                    questGiver.questGiverData.QuestGiverQuests[i] = null;
                    questGiver.questGiverData.QuestGiverQuests.RemoveAt(i);
                    questGiver.Quests[i] = null;
                    questGiver.Quests.RemoveAt(i);
                    questGiver.IsDisplayingQuest = false;
                }
            }
        }
        QuestLog.Instance.RemoveQuest(selectedQuest.QuestScript);
    }
}
