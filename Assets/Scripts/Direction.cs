﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    public bool FacingRight = true;
    public Vector3 scale;

    // Use this for initialization
    void Start ()
    {
        scale = transform.localScale;
    }

    public void CheckDirection(float direction)
    {
        // Checks direction and determines if the sprite should flip or not
        if (direction > 0 && !FacingRight)
        {
            scale.x = -scale.x;
            FacingRight = true;
        }
        else if (direction < 0 && FacingRight)
        {
            scale.x = -scale.x;
            FacingRight = false;
        }
        transform.localScale = scale;
    }
}
