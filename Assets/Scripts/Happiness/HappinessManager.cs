using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappinessManager : MonoBehaviour
{
    [SerializeField] private TurtleTale.SessionData sessionData;

    public void Add(int points)
    {
        sessionData.Happiness += points;

        Debug.Log("Happiness: " + sessionData.Happiness);
    }
}
