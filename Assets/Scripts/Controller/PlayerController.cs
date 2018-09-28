using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Movement movement;
    Direction direction;
    public PlayerChildCollision Body;

    // Use this for initialization
    void Start()
    {
        movement = GetComponent<Movement>();
        direction = GetComponent<Direction>();

        SetGlobalData();
    }

    // Set the player's stats based on the Global Data
    public void SetGlobalData()
    {
        movement.xSpeed = GlobalData.Instance.MovementSpeed;
        movement.ySpeed = GlobalData.Instance.MovementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float xDirection = Input.GetAxis("Horizontal");
        float yDirection = Input.GetAxisRaw("Vertical");
        movement.Move(xDirection, yDirection);
        direction.CheckDirection(xDirection);
    }

    public void OnChildCollision(PlayerChildCollision childPart, Collider2D collider)
    {
        /*
        // Checks collisions with Body
        if (childPart == Body)
        {
            // If player collides with ....
            if (collider.gameObject.layer == 9 || collider.gameObject.layer == 10)
            {

            }
        }
        */
    }


}

