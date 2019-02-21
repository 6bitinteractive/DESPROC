using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Wander))]

public class TurtleController : MonoBehaviour
{
    Wander wander;

    public float Speed;

    private Transform plastic;

    [SerializeField] private LayerMask plasticLayerMask;
    [SerializeField] private TurtleChildCollision body;
    [SerializeField] private TurtleChildCollision sight;

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
    }

    private void DeadState()
    {
        throw new NotImplementedException();
    }

    private void ChokeState()
    {
        throw new NotImplementedException();
    }

    private void WanderState()
    {
        wander.WanderToNewPoint(Speed);
    }

    private void ChaseState()
    {
        Debug.Log("Chasing");
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(plastic.position.x, transform.position.y), Speed * Time.deltaTime); // Move Towards target
        
        // Rotate Towards new Target
        Vector2 direction = plastic.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void OnChildCollision(TurtleChildCollision childPart, Collider2D collider)
    {
        plastic = collider.transform;

        // Checks collisions with Body
        int collisionLayerMask = 1 << collider.gameObject.layer;

        // If collides with Plastic
        if (collisionLayerMask == plasticLayerMask.value)
        {
            // If collides with Body
            if (childPart == body)
            {
              //  curState = FSMState.Choke;
            }

            // If collides with Sight
            if (childPart == sight)
            {
               curState = FSMState.Chase;
            }
        }
    }
}
