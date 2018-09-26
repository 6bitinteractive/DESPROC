using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FallingPlasticController : MonoBehaviour
{
    Movement movement;

    // Use this for initialization
    void Start()
    {
        movement = GetComponent<Movement>();
        movement.Speed = Random.Range(1, 5); // Randomizes speed
        transform.localScale = Vector3.one * Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        movement.Move(0, -1); // Move down
    }
}

