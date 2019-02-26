﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchingMiniGameController : MonoBehaviour
{
    [SerializeField] private GameEvent OnRescuingTurtle;
    [SerializeField] private GameEvent OnTurtleRescued;
    [SerializeField] private LayerMask LayerMask;
    [SerializeField] private AnimationClip rescuingAnimation;

    private GameObject player;
    private Animator animator;
    private float animationDuration;
    private bool isRescuing;

    private void Start()
    {
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
               // Debug.Log(hit.collider.gameObject.name);
                //int collisionLayerMask = 1 << hit.collider.gameObject.layer;

                TurtleController turtleController = hit.collider.gameObject.GetComponent<TurtleController>();

                // If Turtle
                if (turtleController)
                {
                    // If turtle is choking and touches turtle or left clicks turtle
                    if (turtleController.isChoking && Input.GetMouseButtonDown(0))
                    {
                        StartCoroutine(Rescue(turtleController));
                    }
                }
            }   
        }    
    }

    IEnumerator Rescue(TurtleController turtleController)
    {

        OnRescuingTurtle.Raise();
        animator.SetBool("isRescuing", true);
        yield return new WaitForSeconds(rescuingAnimation.length);
        OnTurtleRescued.Raise();
        animator.SetBool("isRescuing", false);
        turtleController.isChoking = false;
    }
}


