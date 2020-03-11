using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Quests
{
    public string QuestGiverName;
    public QuestScript QuestScript { get; set; }
    public QuestGiver QuestGiver{ get; set; }
    public UnityEvent OnQuestAccepted = new UnityEvent();
    private bool isAccepted;
    public bool IsAlreadyDone = false; // Hack: Just so IsComplete can be called correctly on loading a save file

    public bool HasBeenDisplayed; // Hack: avoid redundantly displaying completed quest message feed

    public CollectObjective[] CollectObjectives
    {
        get
        {
            return collectObjectives;
        }

        set
        {
            collectObjectives = value;
        }
    }

    public bool IsComplete
    {
        set
        {
            IsComplete = value;
        }

        get
        {
            if (!IsAlreadyDone)
            {
                foreach (Objective objective in collectObjectives)
                {
                    if (!objective.IsComplete)
                    {
                        return false;
                    }
                }

                IsAlreadyDone = true;
                return true;
            }
            else
                return true;
        }
    }

    public bool IsAccepted
    {
        get
        {
            return isAccepted;
        }

        set
        {
            isAccepted = value;
        }
    }

    public string Name;
    public string Objective;
    [TextArea(15, 20)]
    public string Description;

    [SerializeField] private CollectObjective[] collectObjectives;
}

[System.Serializable]
public abstract class Objective
{
    [SerializeField] protected int amount;
    protected int currentAmount;
    [SerializeField] protected string objectiveTag;

    public string ObjectiveTag
    {
        get
        {
            return objectiveTag;
        }
    }

    public int Amount
    {
        get
        {
            return amount;
        }

        set
        {
            amount = value;
        }
    }

    public int CurrentAmount
    {
        get
        {
            return currentAmount;
        }

        set
        {
            currentAmount = value;
        }
    }

    public bool IsComplete
    {
        set
        {
            IsComplete = value;
        }
        get
        {
            return CurrentAmount >= Amount;
        }
    }
}

[System.Serializable]
public class CollectObjective : Objective
{
    public UnityEvent OnPickup = new UnityEvent();
    public UnityEvent OnCompletion = new UnityEvent();
    string tag;

    public void UpdateItemCount(GameObject objective)
    {
        ObjectiveInteractable oi = objective.GetComponent<ObjectiveInteractable>();
        if (oi != null) tag = oi.name;

        //Checks if the objectives name is the same as the type string
        if (string.Equals(tag, ObjectiveTag))
        {
            OnPickup.Invoke();
            CurrentAmount++;
            Debug.Log(oi.name);
            //Displays message feed
            if (CurrentAmount <= Amount)
            {
                MessageFeedManager.Instance.WriteMessage(string.Format("{0}: {1}/{2}", objective.name, CurrentAmount, Amount));
            }

            QuestLog.Instance.UpdateSelectedQuest();
            QuestLog.Instance.CheckCompletion();

            // if quest is complete
            if (IsComplete)
            {
                CurrentAmount = Amount;
                QuestLog.Instance.UpdateSelectedQuest();
                OnCompletion.Invoke();
            }
        }
    }

    public void CheckItemCount()
    {
        // if quest is complete
        QuestLog.Instance.CheckCompletion();
        QuestLog.Instance.UpdateSelectedQuest();
    }
}

// Reference https://www.youtube.com/watch?v=wClMZ2Rim6w