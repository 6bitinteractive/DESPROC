using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameState : MonoBehaviour
{
    public const int MinTrashToCollect = 30;
    public const float MinTurtlesToSavePercentage = 0.7f;

    public UnityEvent OnWin = new UnityEvent();
    public UnityEvent OnLose = new UnityEvent();

    private int totalTrashCollected;
    private bool hasSavedEnoughTurtles;
    private bool hasRunOutOfTime;

    public void IncreaseTrashCollected()
    {
        totalTrashCollected++;
    }

    public void SetHasSavedEnoughTurtles()
    {
        hasSavedEnoughTurtles = true;
    }

    public void SetHasRunOutOfTime()
    {
        hasRunOutOfTime = true;
    }

    public void CheckGameState()
    {
        bool hasEnoughTrashCollected = totalTrashCollected >= MinTrashToCollect;

        if (hasEnoughTrashCollected && hasSavedEnoughTurtles)
            OnWin.Invoke();
        else if (hasRunOutOfTime)
            OnLose.Invoke();
    }

    public void OnWonDebug()
    {
        Debug.Log("Win");
    }

    public void OnLoseDebug()
    {
        Debug.Log("Lose");
    }
}
