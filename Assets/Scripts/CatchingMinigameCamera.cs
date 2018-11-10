using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchingMinigameCamera : MonoBehaviour
{
    public GameEvent CameraStopped;
    public GameEvent CameraMoving;
    public float Speed = 3f;
    public Transform target;

    void Update()
    {
        Vector3 targetPos = target.position;
        targetPos.z = -10;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Speed * Time.unscaledDeltaTime);

        if (transform.position.y == 0) CameraStopped.Raise();

        else CameraMoving.Raise();
   
    }
}
