using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private Window questWindow; 
    [SerializeField] private List<Quests> quests;
    [SerializeField] private bool isDisplayingQuest;
    private bool isQuestComplete;
   // public TurtleTale.SessionData sessionData;

    public List<Quests> Quests
    {
        get
        {
            return  quests;
        }
    }

    private void Start()
    {
        foreach (Quests quest in quests)
        {
           // sessionData.QuestGiverQuests.Add(i);
            quest.QuestGiver = this;
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
