using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// When timer reaches 0 and there are still turtles alive, check if player did collect some plastic
public class WinCondition : MonoBehaviour
{
    [SerializeField] private TurtleTale.SessionData sessionData;
    [SerializeField] private int requiredPlastic = 3;

    public UnityEvent OnVictory = new UnityEvent();
    public UnityEvent OnGameOver = new UnityEvent();

    public void VerifyWin()
    {
        if (sessionData.CollectedPlastic.Count >= requiredPlastic)
            OnVictory.Invoke();
        else
            OnGameOver.Invoke();
    }
}
