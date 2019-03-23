using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlterSceneOnQuestStatus : MonoBehaviour
{
    public TurtleTale.SessionData sessionData;
    public string questName;

    public UnityEvent OnQuestNotCompleted = new UnityEvent();
    public UnityEvent OnQuestCompleted = new UnityEvent();

    public void Awake()
    {
        if (QuestLog.Instance != null)
        {
            if ((QuestLog.Instance.sessionData.Quests == null))
            {
                //Debug.Log("1");
            }
            // If  quest name is empty or doesn't match any quest on the quest log
            else if ((QuestLog.Instance.sessionData.Quests.Exists(x => x.Name == questName) == false))
            {
                //Debug.Log("2");
            }
            // If quest name matches a quest on the quest list
            else if (QuestLog.Instance.sessionData.Quests.Exists(x => x.Name == questName))
            {
                for (int i = 0; i < QuestLog.Instance.sessionData.Quests.Count; i++)
                {
                    // If quest exists and is not complete
                    if ((QuestLog.Instance.sessionData.Quests[i].Name == questName) && (QuestLog.Instance.sessionData.Quests[i].IsComplete == false))
                    {
                        OnQuestNotCompleted.Invoke();
                    }
                    else if ((QuestLog.Instance.sessionData.Quests[i].Name == questName) && (QuestLog.Instance.sessionData.Quests[i].IsComplete == true))
                    {
                        OnQuestCompleted.Invoke();
                    }
                }
            }
        }
    }
}
