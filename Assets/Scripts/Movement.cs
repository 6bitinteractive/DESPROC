using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float xSpeed;
    public float ySpeed;

    private Rigidbody2D rb;
    private Animator animator;

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
            animator.SetFloat("xSpeed", Mathf.Abs(rb.velocity.x));
            animator.SetFloat("ySpeed", Mathf.Abs(rb.velocity.y));
        }

        // Moves the entity
        rb.velocity = new Vector3(xDirection * xSpeed, yDirection * ySpeed);
    }
}
