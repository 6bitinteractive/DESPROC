using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleManager : MonoBehaviour
{
    public static int TotalTurtles { get; private set; }

    private void Awake()
    {
        // Check if all children are, indeed, turtles
        int total = transform.childCount;

        for (int i = 0; i < total; i++)
        {
            TurtleInteractable turtle = transform.GetChild(i).GetComponent<TurtleInteractable>();

            if (turtle == null)
                Debug.LogError("TurtleManager: Only objects of type TurtleInteractable are allowed to be a child of this transform.");
            else
                TotalTurtles = total;
        }
    }
}
