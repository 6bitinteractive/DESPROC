using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistance : MonoBehaviour
{
    public GameEvent EnterAreaPrompt;
    public GameEvent ExitAreaPrompt;
    public float MinimumDistance;
    public Transform TargetPosition;

    private float currentDistance;

    void Update()
    {
        currentDistance = Vector2.Distance(transform.position, TargetPosition.position);
        // Debug.Log(currentDistance);

        if (currentDistance <= MinimumDistance) EnterAreaPrompt.Raise();
 
        else if (currentDistance >= MinimumDistance) ExitAreaPrompt.Raise();
    }
}
