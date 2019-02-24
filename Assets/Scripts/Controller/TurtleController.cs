using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Wander))]

public class TurtleController : MonoBehaviour
{
    Wander wander;

    public float Speed;
    public float ChokeDuration = 5;

    private bool isChoking;
    private Collider2D plastic;
    private Rigidbody2D plasticRB;

    [SerializeField] private LayerMask plasticLayerMask;
    [SerializeField] private TurtleChildCollision body;
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
    void Awake ()
    {
        wander = GetComponent<Wander>();
        curState = FSMState.Wander;
    }

	// Update is called once per frame
	void Update ()
    {
        switch (curState)
        {
            case FSMState.Wander: WanderState(); break;
            case FSMState.Chase: ChaseState(); break;
            case FSMState.Choke: ChokeState(); break;
            case FSMState.Dead: DeadState(); break;
        }

        //Debugging Purposes
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isChoking = false;
        }
    }

    private void DeadState()
    {
       Debug.Log("Dead");
     //  OnDeath.Raise();
    }

    private void ChokeState()
    {
      
        if (isChoking)
        {
            StartCoroutine(Choke());
          //  OnChoke.Raise();
        }

        else curState = FSMState.Wander;
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
        plastic = collider;
        plasticRB = plastic.GetComponent<Rigidbody2D>();

        int collisionLayerMask = 1 << collider.gameObject.layer;

        // Check if already choking
        if (!isChoking)
        {
            // If collides with Plastic
            if (collisionLayerMask == plasticLayerMask.value)
            {
                // If collides with Body
                if (childPart == body)
                {
                    isChoking = true;
                    curState = FSMState.Choke;
                    plasticRB.constraints = RigidbodyConstraints2D.FreezePosition;
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
            yield return new WaitForSeconds(ChokeDuration);
            plastic.gameObject.SetActive(false);
            curState = FSMState.Dead;
        }
    }
}
