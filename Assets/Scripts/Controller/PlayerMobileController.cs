using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is a placeholder for playermobilecontroller simply to test tap to move
public class PlayerMobileController : MonoBehaviour
{
    [SerializeField] private TurtleTale.SessionData sessionData;
    [SerializeField] private LayerMask LayerMask;
    [SerializeField] private Camera MainCamera;

    private Movement movement;
    private Direction direction;
    private PlayerController playerController;
    private Vector3 targetPos;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        direction = GetComponent<Direction>();
        playerController = GetComponent<PlayerController>();
        playerController.enabled = false;
        targetPos = transform.position; // quick fix to prevent player from moving to vector(0,0,0) on start
    }

    private void Update()
    {
        if (movement.enabled == true && Input.GetMouseButtonDown(0))
        {
            CastRay();
        }
    }

    void FixedUpdate()
    {
        // Move towards target position
        if (transform.position.x != targetPos.x)
        {
            Move();

            /*
            // Reached target destination
            if (transform.position.x == targetPos.x)
            {
                Debug.Log("Reached target destination");
            }
            */
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, movement.xSpeed * Time.deltaTime);
        movement.TapToMove(targetPos.x, targetPos.y);
    }

    void CastRay()
    {
        Vector3 worldPoint = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = MainCamera.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, LayerMask);

        if (hit)
        {
            targetPos = hit.point; // Set target pos
            direction.CheckDirection(targetPos.x); // Face target pos
        }
    }
}
