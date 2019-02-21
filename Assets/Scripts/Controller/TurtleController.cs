using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Wander))]

public class TurtleController : MonoBehaviour
{
    Wander wander;

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
        wander.WanderToNewPoint();
    }

    private void ChaseState()
    {
        throw new NotImplementedException();
    }
}
