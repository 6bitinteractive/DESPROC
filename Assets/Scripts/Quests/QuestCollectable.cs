using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestCollectable : Quest
{
    public string ItemName;
    public int CurrentItemCount = 0;
    public int RequiredItemCount = 50;
    public UnityEvent OnPickup = new UnityEvent();
    public UnityEvent OnCompletion = new UnityEvent();

    private QuestManager QuestManager;

    private void Awake()
    {
        QuestManager = GetComponent<QuestManager>();
    }


    public override bool IsComplete() {
        return (CurrentItemCount >= RequiredItemCount);
    }

    public override void Complete()
    {
        OnCompletion.Invoke();
        //Add towards total score for endings?
    }

    public override void DrawHUD()
    {
        // Temporary place holder
        //GUILayout.BeginArea(new Rect(375, 0, 200, 200));
        GUILayout.Label(string.Format("Collected {0}/{1} " + ItemName, CurrentItemCount, RequiredItemCount));
        // GUILayout.EndArea();
    }

    public void AddToTotalItemCount(GameObject ItemReference)
    {
        Debug.Log(gameObject.name);
        if (string.Equals(ItemReference.name, ItemName))
        {
            Debug.Log(gameObject.name);
            CurrentItemCount++;
            QuestManager.CheckQuestStatus();
            OnPickup.Invoke();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (string.Equals(collision.gameObject.name, ItemName))
        {
            Debug.Log(gameObject.name);
            CurrentItemCount++;
            QuestManager.CheckQuestStatus();
            OnPickup.Invoke();
        }
    }
}
