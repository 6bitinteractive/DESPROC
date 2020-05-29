using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchingMinigameCamera : MonoBehaviour
{
    public GameEvent CameraStopped;
    public GameEvent CameraMoving;
    public float Speed = 3f;
    public Transform target;

    private void Awake()
    {
        // To resize the initial camera so that the sides of the boundry is not seen
        float aspectRatio = (16f / 9f);
        this.GetComponent<Camera>().orthographicSize = 5 * (aspectRatio / Camera.main.aspect);
    }

    void Update()
    {
        Vector3 targetPos = target.position;
        targetPos.z = -10;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Speed * Time.unscaledDeltaTime);

        if (transform.position.y == 0) CameraStopped.Raise();

        else CameraMoving.Raise();
   
    }
}
