using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Reference https://forum.unity.com/threads/goals-missions-objective-system.246842/

public class QuestManager : MonoBehaviour
{
    public List<Quest> Quests = new List<Quest>();
    public int TotalQuests;
    public int CurrentQuestCount;
    public UnityEvent OnRequirementMet = new UnityEvent();

    public bool IsRequirementMet()
    {
        return (CurrentQuestCount >= TotalQuests);
    }

    void Awake()
    {
        Quests = new List<Quest>(GetComponents<Quest>());
        TotalQuests = Quests.Count;
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
        // Check each quest
        for (var i = Quests.Count - 1; i > -1; i--)
        {
            //If a quest is complete
            if (Quests[i].IsComplete())
            {
                CurrentQuestCount++; // Add to current quest count
                Destroy(Quests[i]);
                Quests.RemoveAt(i); // Remove quest from list

                // If all quests are complete
                if (IsRequirementMet())
                {
                    OnRequirementMet.Invoke();
                }
            }
        }
    }
}
