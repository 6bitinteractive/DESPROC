using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchingMiniGameController : MonoBehaviour
{
    [SerializeField] private TurtleTale.SessionData sessionData;
    [SerializeField] private LayerMask LayerMask;
    [SerializeField] private AnimationClip rescuingAnimation;
    [SerializeField] private float pickUpRange = 1.5f;

    private GameObject player;
    private Rigidbody2D rb;
    private Animator animator;
    private Movement movement;
    private Direction direction;
    private Vector3 targetPos;
    private float animationDuration;
    private bool isRescuing;
    private bool isMoving;
    
    private GameObject collectedPlastic;

    private void Start()
    {
        player = gameObject;
        movement = GetComponent<Movement>();
        direction = GetComponent<Direction>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastMovementRay();
            CastRay();
        }
    }

    private void FixedUpdate()
    {
        // Move towards target position
        if (isMoving)
        {
            Move();
        }
    }

    void CastRay()
    {
        // Create ray cast from mouse input
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane playerPlane = new Plane(Vector3.back, player.transform.position); // Set player
        float distance;

        // Creates a ray cast from player to mouse click
        if (playerPlane.Raycast(ray, out distance))
        {
            Vector3 hitPt = ray.GetPoint(distance);
            Ray playerToClick = new Ray(player.transform.position, hitPt - player.transform.position);
            RaycastHit2D hit = Physics2D.Raycast(playerToClick.origin, playerToClick.direction, pickUpRange, LayerMask);

            // Debug.DrawRay(playerToClick.origin, playerToClick.direction * 50, Color.yellow);
            if (hit)
            {
                Debug.Log(hit.collider.gameObject.name);
                //int collisionLayerMask = 1 << hit.collider.gameObject.layer;

                TurtleController turtleController = hit.collider.gameObject.GetComponent<TurtleController>();
                FallingPlasticController fallingPlasticController = hit.collider.gameObject.GetComponent<FallingPlasticController>();

                // If Turtle
                if (turtleController)
                {
                    // If turtle is choking and touches turtle or left clicks turtle
                    if (turtleController.isChoking && Input.GetMouseButtonDown(0))
                    {
                        animator.SetBool("isRescuing", true);
                        turtleController.plasticInContact.ShowOutline(true); // Show the outline similar to pickup
                        StartCoroutine(Rescue(turtleController));
                    }
                }

                // If Falling Plastic
                if (fallingPlasticController)
                {
                    animator.SetBool("isRescuing", true);
                    rb.velocity = Vector3.zero;
                    fallingPlasticController.PickUp();
                    StartCoroutine(PickupPlastic(fallingPlasticController));
                }   
            }
        }
    }

    IEnumerator PickupPlastic(FallingPlasticController fallingPlasticController)
    {
        collectedPlastic = fallingPlasticController.gameObject;

        // Show an outline to the plastic being picked up
        fallingPlasticController.ShowOutline();

        yield return new WaitForSeconds(rescuingAnimation.length);
        animator.SetBool("isRescuing", false);
        enabled = true;
        GetComponent<Movement>().enabled = true;
        fallingPlasticController.onPickup.Raise();
        fallingPlasticController.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        fallingPlasticController.gameObject.SetActive(false);
    }

    IEnumerator Rescue(TurtleController turtleController)
    {
        yield return new WaitForSeconds(rescuingAnimation.length);
        enabled = true;
        GetComponent<Movement>().enabled = true;
        animator.SetBool("isRescuing", false);
        turtleController.isChoking = false;
    }

    // Hack: add plastic to sessionData list
    public void AddPlasticToSessionData()
    {
        if (collectedPlastic)
            sessionData.CollectedPlastic.Add(collectedPlastic.GetComponent<Plastic>().PlasticData);
    }

    public void ClearPlasticReference()
    {
        collectedPlastic = null;
    }

    void CastMovementRay()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit)
        {
            targetPos = hit.point; // Set target pos
            direction.CheckDirection(targetPos.x); // Face target pos
            isMoving = true;
        }
    }

    private void Move()
    {
        movement.TapToMove(targetPos);
    }
}
