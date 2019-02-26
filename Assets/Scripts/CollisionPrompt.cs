using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]

public class CollisionPrompt : MonoBehaviour
{
    public UnityEvent OnCollision = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If colliding with player
        if (collision.gameObject.layer == 8)
        {
            OnCollision.Invoke();
        }
    }
}
