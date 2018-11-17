using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleManager : MonoBehaviour
{
    public static int Total { get; private set; }
    private TurtleInteractable[] Turtles;

    private void Awake()
    {
        Turtles = GetComponentsInChildren<TurtleInteractable>();
        Total = Turtles.Length;
    }
}
