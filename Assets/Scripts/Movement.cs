﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float xSpeed;
    public float ySpeed;

    private Rigidbody2D rb;
    private Animator animator;
    private float LastXDirection;
    private float LastYDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Move(float xDirection, float yDirection)
    {
        // Checks if player then player animations (Note this is a placeholder since other moving entites dont have animations yet)
        if (gameObject.layer == 8)
        {
           // float heading = Vector3.Normalize(xDirection + yDirection);
            //animator.SetFloat("xDirection", Mathf.Abs(rb.velocity.x));
          //  animator.SetFloat("yDirection", Mathf.Abs(rb.velocity.y));

             UpdateAnimation(xDirection, yDirection); 
        }

        // Moves the entity
        rb.velocity = new Vector3(xDirection * xSpeed, yDirection * ySpeed);
    }

    private void UpdateAnimation(float xDirection, float yDirection)
    {
        // Check if no longer moving
        if (xDirection == 0 && yDirection == 0)
        {
            // Set last directions
            animator.SetFloat("LastXDirection", LastXDirection);
            animator.SetFloat("LastYDirection", LastYDirection);

            // Become idle
            animator.SetBool("isMoving", false);
        }

        // Else if moving
        else
        {
            // Set Last directions
            LastXDirection = xDirection;
            LastYDirection = yDirection;

            // Move sprite
            animator.SetBool("isMoving", true);
        }

        animator.SetFloat("xDirection", xDirection);
        animator.SetFloat("yDirection", yDirection);

    }
}
