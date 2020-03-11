using System.Collections;
using System.Collections.Generic;
using TurtleTale;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private Window questWindow; 
    [SerializeField] private List<Quests> quests;
    [SerializeField] private bool isDisplayingQuest;
    public QuestGiverData questGiverData;

    public SessionData sessionData;

    public List<Quests> Quests
    {
        get
        {
            return quests;
        }
    }

    public void Awake()
    {
        // Search through session data and add quest giver data
        for (int i = 0; i < sessionData.QuestGiverDatas.Count; i++)
        {
            if (sessionData.QuestGiverDatas[i].QuestGiverName == gameObject.name)
            {
                questGiverData = sessionData.QuestGiverDatas[i];
            }
        }
    }
        
    public void OnEnable()
    {   
        // Create quest giver data if null
        if (questGiverData == null)
        {
            questGiverData = QuestGiverData.CreateInstance(false);
            sessionData.QuestGiverDatas.Add(questGiverData);
            questGiverData.QuestGiverName = gameObject.name;  
        }
    }
    
    private void Start()
    {
        // Set the quest giver data once
        if (questGiverData != null)
        {
            if (!questGiverData.IsQuestSet)
            {
                foreach (Quests quest in quests)
                {
                    //Set quest giver name to this object
                    quest.QuestGiverName = gameObject.name;
                    questGiverData.QuestGiverQuests.Add(quest);
                }

                quests.Clear();

                for (int i = 0; i < questGiverData.QuestGiverQuests.Count; i++)
                {
                    if (questGiverData.QuestGiverQuests[i].QuestGiverName == gameObject.name)
                    {
                        if (quests != null)
                        {
                            quests.Add(questGiverData.QuestGiverQuests[i]);
                        }
                    }
                }
                questGiverData.IsQuestSet = true;
            }

            foreach (Quests quest in quests)
            {
                quest.QuestGiver = this;
            }
        }
    }

    public void AcceptFirstQuest()
    {
        QuestLog.Instance.AcceptQuest(quests[0]);
    }

    public bool IsDisplayingQuest
    {
        get
        {
            return isDisplayingQuest;
        }

        set
        {
            isDisplayingQuest = value;
        }
    }

    // I swear what does this even do - Pado
    public void UpdateQuestStatus()
    {
        {
            foreach (Quests quest in quests)
            {
                if (quest != null)
                {
                    if (quest.IsComplete && QuestLog.Instance.HasQuest(quest))
                    {
                        Debug.Log("Updated Quest");
                    }
                }
            }
        }
    }
}
