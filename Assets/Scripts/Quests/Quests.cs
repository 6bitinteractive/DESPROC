using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Quests
{
    public QuestScript QuestScript { get; set; }

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
        get
        {
            foreach (Objective objective in collectObjectives)
            {
                if (!objective.IsComplete)
                {
                    return false;
                }
            }

            return true;
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

    public void UpdateItemCount(GameObject objective)
    {
        //   Debug.Log("nep");
        //   Debug.Log(ItemReference.name);
        // Checks if it has already been picked up
     
        //Checks if the objectives name is the same as the type string
        if (string.Equals(objective.name, ObjectiveTag))
        {
            OnPickup.Invoke();
            CurrentAmount++;
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
    
}

// Reference https://www.youtube.com/watch?v=wClMZ2Rim6w