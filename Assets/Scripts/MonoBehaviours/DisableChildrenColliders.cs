using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableChildrenColliders : MonoBehaviour
{
    private void Start()
    {
        Collider2D[] children = GetComponentsInChildren<Collider2D>();

        for (int i = 0; i < children.Length; i++)
        {
            children[i].enabled = false;
        }
    }
}
