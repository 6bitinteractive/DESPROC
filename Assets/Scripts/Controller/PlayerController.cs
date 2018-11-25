using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Direction))]
[RequireComponent(typeof(PlayerDataHandler))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TurtleTale.SessionData sessionData;

    Movement movement;
    Direction direction;
    //PlayerData playerData;

    public PlayerChildCollision Body;
    public bool isInMiniGame = false;

    void Start()
    {
        movement = GetComponent<Movement>();
        direction = GetComponent<Direction>();
        //playerData = GetComponent<PlayerDataHandler>().playerData;

        SetGlobalData();
    }

    // Set the player's stats based on the Global Data
    public void SetGlobalData()
    {
        movement.xSpeed = isInMiniGame ? 5f : sessionData.MovementSpeed;
        if (!isInMiniGame) movement.ySpeed = sessionData.MovementSpeed;
    }

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

