﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestScript : MonoBehaviour
{
    public Quests Quest { get; set; }
    private bool isMarkedComplete = false;
    private bool hasBeenDisplayed;

    public void Select()
    {
        GetComponent<Text>().color = Color.yellow;
        QuestLog.Instance.DisplayDescription(Quest);
    }

    public void Deselect()
    {

        GetComponent<Text>().color = Color.white;
    }

    public void IsComplete()
    {
        if (Quest.IsComplete && !isMarkedComplete)
        {
            isMarkedComplete = true;
            GetComponent<Text>().text += "(Complete)";
        }
    }

    public void IsCompleteMessageFeed()
    {
        if (Quest.IsComplete && isMarkedComplete)
        {
            //Hack: to avoid displaying this twice
            if (!hasBeenDisplayed)
            {
                MessageFeedManager.Instance.WriteMessage(string.Format("{0} (Complete)", Quest.Name));
                hasBeenDisplayed = true;
            }
        }
    }
}
