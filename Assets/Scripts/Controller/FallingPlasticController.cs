using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FallingPlasticController : MonoBehaviour
{
    public GameEvent onPickup;
    private Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void PickUp()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
    }
}

