using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool isAnimated = false;
    public float xSpeed;
    public float ySpeed;

    private Rigidbody2D rb;
    private Animator animator;
    private float LastXDirection;
    private float LastYDirection;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Move(float xDirection, float yDirection)
    {
        // If the entity has animations then animate
        if (isAnimated)
        {
            //float heading = Vector3.Normalize(xDirection + yDirection);
            //animator.SetFloat("xDirection", Mathf.Abs(rb.velocity.x));
            //animator.SetFloat("yDirection", Mathf.Abs(rb.velocity.y));

             UpdateAnimation(xDirection, yDirection); 
        }

        // Moves the entity
        rb.velocity = new Vector3(xDirection * xSpeed, yDirection * ySpeed);
    }

    public void TapToMove(Vector3 targetPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, xSpeed * Time.deltaTime);   
        UpdateTapAnimation(targetPos);
    }

    public void ResetVelocity()
    {
        rb.velocity = Vector3.zero;
    }

    public void StopMovingAnimation()
    {
        animator.SetBool("isMoving", false);
    }

    public void DisableMovement()
    {
        StopMovingAnimation();
        rb.velocity = Vector3.zero;
        enabled = false;
    }

    public void EnableMovement()
    {
        enabled = true;
    }

    private void UpdateTapAnimation(Vector3 targetPos)
    {
        // Check if no longer moving
        if (targetPos == transform.position)
        {
            // Become idle
            animator.SetBool("isMoving", false);
        }

        // Else if moving
        else
        {
            // Move sprite
            animator.SetBool("isMoving", true);
        }
    }

    private void UpdateAnimation(float xDirection, float yDirection)
    {
        // Check if no longer moving
        if (xDirection == transform.position.x && yDirection == transform.position.y)
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
