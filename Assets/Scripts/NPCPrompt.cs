using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPrompt : MonoBehaviour
{
    public GameEvent OnNPCCollision;
    public GameEvent OnNPCCollisionExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If colliding with player
        if (collision.gameObject.layer == 8)
        {
            OnNPCCollision.Raise();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If colliding with player
        if (collision.gameObject.layer == 8)
        {
            OnNPCCollisionExit.Raise();
        }
    }
}
