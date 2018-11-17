using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleManager : MonoBehaviour
{
    public static int Total { get; private set; }
    private TurtleInteractable[] Turtles;

    private void Awake()
    {
        int Total = transform.childCount;
        Turtles = GetComponentsInChildren<TurtleInteractable>();
    }
}
