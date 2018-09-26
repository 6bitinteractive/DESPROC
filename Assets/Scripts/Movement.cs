using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float Speed;

    private Rigidbody2D rb;
    // private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = this.GetComponent<Animator>();
    }

    public void Move(float xDirection, float yDirection)
    {
        //animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        // Moves the entity
        rb.velocity = new Vector3(xDirection * Speed, yDirection * Speed);
    }
}
