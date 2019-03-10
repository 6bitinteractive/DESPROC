using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappinessProvider : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;
    [SerializeField] private int happinessValue;

    public void GainHappinessValue()
    {
        gameEvent.sentInt = happinessValue;
        gameEvent.Raise();
        Debug.Log("Gained " + happinessValue + " happiness points.");
    }
}
