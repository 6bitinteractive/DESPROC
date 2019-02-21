using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleChildCollision : MonoBehaviour {

    TurtleController parent;

    void Start()
    {
        parent = GetComponentInParent<TurtleController>();
    }

    // Pass reference to parent
    void OnTriggerEnter2D(Collider2D collision)
    {
        parent.OnChildCollision(this, collision);
    }
}
