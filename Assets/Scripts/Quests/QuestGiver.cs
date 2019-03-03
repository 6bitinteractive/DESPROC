using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private Window questWindow; 
    [SerializeField] private Quests[] quests;
    [SerializeField] private bool isDisplayingQuest;

    public Quests[] Quests
    {
        get
        {
            return quests;
        }
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


    /*
    private void Awake()
    {
        tmpLog.AcceptQuest(quests[0]);
        tmpLog.AcceptQuest(quests[1]);
    }
    */
}
