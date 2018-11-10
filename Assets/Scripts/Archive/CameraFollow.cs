using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float speed = 3f;
    public Transform target;

    void Update()
    {
        Vector3 targetPos = target.position;
        targetPos.z = -10;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.unscaledDeltaTime);
    }
}
