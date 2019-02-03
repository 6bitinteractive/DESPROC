using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Reference https://forum.unity.com/threads/goals-missions-objective-system.246842/

public class QuestManager : MonoBehaviour
{
    public Quest[] Quests;

    void Awake()
    {
        Quests = GetComponents<Quest>();
    }

    void OnGUI()
    {
        foreach (var quest in Quests)
        {
           quest.DrawHUD();
        }
    }

    public void CheckQuestStatus()
    {
        foreach (var quest in Quests)
        {
            if (quest.IsComplete())
            {
                quest.Complete();
                Destroy(quest);
            }
        }
    }
}
