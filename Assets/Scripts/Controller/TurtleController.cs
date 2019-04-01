using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Wander))]

public class TurtleController : MonoBehaviour
{
    Wander wander;
    public Image ChokeUI;

    public float Speed;
    public float ChokeDuration = 5;
    public bool isChoking;
    public FallingPlasticController plasticInContact { get; private set; } // This is to get the reference on which plastic is the turtle choking

    private float CurrentChokeDuration;
    private Collider2D plastic;
    private Rigidbody2D plasticRB;
    private Animator animator;
    private int counter = 1;

    [SerializeField] private LayerMask plasticLayerMask;
    [SerializeField] private TurtleChildCollision mouth;
    [SerializeField] private TurtleChildCollision sight;
    [SerializeField] private GameEvent OnDeath;
    [SerializeField] private GameEvent OnChoke;

    public enum FSMState
    {
        Wander,
        Chase,
        Choke,
        Dead,
    }

    public FSMState curState; // current state

    // Use this for initialization
    void Awake()
    {
        wander = GetComponent<Wander>();
        animator = GetComponent<Animator>();

        CurrentChokeDuration = ChokeDuration;
        curState = FSMState.Wander;
    }

    // Update is called once per frame
    void Update()
    {
        switch (curState)
        {
            case FSMState.Wander: WanderState(); break;
            case FSMState.Chase: ChaseState(); break;
            case FSMState.Choke: ChokeState(); break;
            case FSMState.Dead: DeadState(); break;
        }
    }

    private void DeadState()
    {
        // Do once
        if (counter == 1)
        {
            counter++;
            DisableColliders();
            animator.SetTrigger("Die");
            OnDeath.Raise();
        }
    }

    private void ChokeState()
    {
        //Display the ChokeUI
        ChokeUI.gameObject.SetActive(true);

        if (isChoking && plastic.gameObject.activeSelf)
        {
            StartCoroutine(Choke());
            animator.SetBool("isChoking", true);
            //OnChoke.Raise();
        }
        else
        {
            StopAllCoroutines(); // Stop choking
            CurrentChokeDuration = ChokeDuration; //Reset current choke duration
            ChokeUI.gameObject.SetActive(false); // Hide choke ui
            animator.SetBool("isChoking", false);
            isChoking = false;
            plastic.gameObject.SetActive(false); // Remove plastic
            plasticInContact.ResetObject(); // Reset plastic's outline
            plasticInContact = null; // Remove the reference
            curState = FSMState.Wander; // Wander
        }
    }

    private void WanderState()
    {
        wander.WanderToNewPoint(Speed);
    }

    private void ChaseState()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(plastic.transform.position.x, plastic.transform.position.y), Speed * Time.deltaTime); // Move Towards target

        // Rotate Towards new Target
        Vector2 direction = plastic.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void OnChildCollision(TurtleChildCollision childPart, Collider2D collider)
    {
        int collisionLayerMask = 1 << collider.gameObject.layer;

        // Check if already choking
        if (!isChoking)
        {
            // If collides with PlasticData
            if (collisionLayerMask == plasticLayerMask.value)
            {
                plastic = collider;

                // If collides with Mouth
                if (childPart == mouth)
                {
                    isChoking = true;
                    curState = FSMState.Choke;

                    plasticRB = plastic.GetComponent<Rigidbody2D>();
                    plasticRB.constraints = RigidbodyConstraints2D.FreezePosition;

                    // Show an outline to easily see on which plastic is the turtle choking
                    plasticInContact = plastic.GetComponent<FallingPlasticController>();
                    plasticInContact.ShowOutline(false);
                }

                // If collides with Sight
                if (childPart == sight)
                {
                    curState = FSMState.Chase;
                }
            }
        }
    }

    IEnumerator Choke()
    {
        while (isChoking)
        {
            // Update choker timer ui (updating ui in a controller disgusting... but oh well)
            if (CurrentChokeDuration != ChokeUI.fillAmount)
            {
                CurrentChokeDuration -= Time.deltaTime;
                ChokeUI.fillAmount = CurrentChokeDuration / ChokeDuration;
            }

            yield return new WaitForSeconds(ChokeDuration);
            plastic.gameObject.SetActive(false);
            curState = FSMState.Dead;
        }
    }

    public void DisableColliders()
    {
        mouth.gameObject.SetActive(false);
        sight.gameObject.SetActive(false);
    }
}
