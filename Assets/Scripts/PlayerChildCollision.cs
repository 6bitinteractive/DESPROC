using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChildCollision : MonoBehaviour {

    PlayerController parent;

    void Start()
    {
        parent = GetComponentInParent<PlayerController>();
    }

    // Pass reference to parent
    void OnTriggerEnter2D(Collider2D collision)
    {
        parent.OnChildCollision(this, collision);
    }
}
