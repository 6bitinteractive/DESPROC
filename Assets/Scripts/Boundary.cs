using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    public GameEvent DisableRandom;
    void OnTriggerEnter2D(Collider2D collider)
    {
        collider.gameObject.SetActive(false);
        DisableRandom.Raise();
    }
}
