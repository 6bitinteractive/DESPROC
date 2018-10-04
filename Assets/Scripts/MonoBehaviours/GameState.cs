using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public const int MinTrashToCollect = 30;
    public const float MinTurtlesToSavePercentage = 0.7f;
    public SceneHandler SceneHandler; // temporary pix Remove later

    public UnityEvent OnWin = new UnityEvent();
    public UnityEvent OnLose = new UnityEvent();
    public Text TotalTrashCollectedText;
    public Text MinTrashToCollectText;

    private int totalTrashCollected;
    private bool hasSavedEnoughTurtles;
    private bool hasRunOutOfTime;

    public void Update()
    {
        DisplayTotalTrash();
    }

    public void DisplayTotalTrash()
    {
        TotalTrashCollectedText.text = totalTrashCollected.ToString();
        MinTrashToCollectText.text = " / " + MinTrashToCollect.ToString();
    }

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
        {
            OnWin.Invoke();
            SceneHandler.LoadScene("Victory"); //temporary fix remove later reeeee
        }
       
        else if (hasRunOutOfTime)
            OnLose.Invoke();
    }
}
