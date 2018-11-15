using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool Collected = false;
    protected float OriginalBuffValue;
    [SerializeField]
    protected float Duration;
    [SerializeField]
    protected float BuffValue;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // If collides with player
        if (collision.gameObject.layer == 8 && Collected == false)
        {
            StartCoroutine(BuffTimer(collision));
            Collected = true; // prevents an item from being collected more than once
        }
    }

    protected virtual IEnumerator BuffTimer(Collider2D collision)
    {
        yield return new WaitForSeconds(Duration);
        EndBuffTimer();
    }

    protected virtual void EndBuffTimer()
    {
        // Reset buff
        gameObject.SetActive(false);
        Collected = false;    
    }
}
