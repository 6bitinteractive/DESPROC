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
           // float heading = Vector3.Normalize(xDirection + yDirection);
            //animator.SetFloat("xDirection", Mathf.Abs(rb.velocity.x));
          //  animator.SetFloat("yDirection", Mathf.Abs(rb.velocity.y));

             UpdateAnimation(xDirection, yDirection); 
        }

        // Moves the entity
        rb.velocity = new Vector3(xDirection * xSpeed, yDirection * ySpeed);
    }

    public void ResetVelocity()
    {
        rb.velocity = Vector3.zero;
    }

    public void MoveToPosition(GameObject target)
    {
        StartCoroutine(AddForce(target));
    }

    private IEnumerator AddForce(GameObject target)
    {
        Vector2 force;
        // Determine vector of force based on target's position relative to game object
        if(target.transform.position.x < transform.position.x)
        {
            force = new Vector2(-1, 0);
        }
        else
        {
            force = new Vector2(0, -1);
        }

        while (transform.position != target.transform.position)
        {
            rb.AddForce(force * 2);
            yield return null;
        }
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
