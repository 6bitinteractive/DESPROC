using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private Window questWindow;
    [SerializeField] private Quests[] quests;
    [SerializeField] private bool isDisplayingQuest;
    [SerializeField] private TurtleTale.SessionData sessionData;

    public Quests[] Quests
    {
        get
        {
            return quests;
            //return quests;
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
}
