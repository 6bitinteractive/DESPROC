using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchingMiniGameController : MonoBehaviour
{
    [SerializeField] private LayerMask LayerMask;
    [SerializeField] private AnimationClip rescuingAnimation;

    private GameObject player;
    private Animator animator;
    private float animationDuration;
    private bool isRescuing;

    private TurtleTale.SessionData sessionData;
    private GameObject collectedPlastic;

    private void Start()
    {
        sessionData = GetComponent<PlayerController>().sessionData;
        player = gameObject;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
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
            RaycastHit2D hit = Physics2D.Raycast(playerToClick.origin, playerToClick.direction, 2.5f, LayerMask);

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
                        StartCoroutine(Rescue(turtleController));
                    }
                }

                // If Falling Plastic
                if (fallingPlasticController)
                {
                    animator.SetBool("isRescuing", true);
                    fallingPlasticController.PickUp();
                    StartCoroutine(PickupPlastic(fallingPlasticController));
                }
            }
        }
    }

    IEnumerator PickupPlastic(FallingPlasticController fallingPlasticController)
    {
        collectedPlastic = fallingPlasticController.gameObject;

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
}
