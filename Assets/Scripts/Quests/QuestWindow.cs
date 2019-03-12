using System.Collections;
using System.Collections.Generic;
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
            foreach (Quests quest in questGiver.Quests)
            {
                if (quest != null)
                {
                    GameObject gameObject = Instantiate(questPrefab, questArea);
                    gameObject.GetComponent<Text>().text = quest.Name; // Display quest name
                    gameObject.GetComponent<QuestGiverQuestScript>().Quest = quest; // Set quest

                    quests.Add(gameObject); // Add to list


                    for (int i = 0; i < QuestLog.Instance.sessionData.Quests.Count; i++)
                    {
                        // If quest is complete
                        if (QuestLog.Instance.HasQuest(quest) && QuestLog.Instance.sessionData.Quests[i].IsComplete)
                        {
                            gameObject.GetComponent<Text>().color = Color.yellow;
                        }
                    }
                   
                    // If quest is accepted
                    if (QuestLog.Instance.HasQuest(quest))
                    {
                        Color color = gameObject.GetComponent<Text>().color;
                        color.a = 0.5f;
                        gameObject.GetComponent<Text>().color = color;
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


            backButton.SetActive(true); // Display back button
            questArea.gameObject.SetActive(false); // Hide quest
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
            questGiver.IsDisplayingQuest = false;
        }
    }

    public void CompleteQuest()
    {
        if (selectedQuest.IsComplete)
        {
            for (int i = 0; i < questGiver.Quests.Count; i++)
            {
                // If the quest is the same as the selected
                if (selectedQuest == questGiver.Quests[i])
                {
                    questGiver.Quests[i] = null;
                    questGiver.IsDisplayingQuest = false;
                }
            }
        }
        QuestLog.Instance.RemoveQuest(selectedQuest.QuestScript);
    }
}
