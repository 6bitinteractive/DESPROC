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
    [TextArea(15, 20)]
    public string Description;

    [SerializeField] private CollectObjective[] collectObjectives;
}

[System.Serializable]
public abstract class Objective
{
    [SerializeField] private int amount;
    private int currentAmount;
    [SerializeField] private string type;
    private bool isCollected;

    public string Type
    {
        get
        {
            return type;
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
    private bool isCollected;
    public void UpdateItemCount(GameObject ItemReference)
    {
        //   Debug.Log("nep");
        //   Debug.Log(ItemReference.name);

        // Checks if it has already been picked up
        if (isCollected == false)
        {
            //Checks if the collectable's name is the same as the type
            if (string.Equals(ItemReference.name, Type))
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
}

// Reference https://www.youtube.com/watch?v=wClMZ2Rim6w