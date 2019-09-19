using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    private bool FacingRight = true;
    private bool IsFlippable = true;
    private Vector3 scale;

    // Use this for initialization
    void Start ()
    {
        scale = transform.localScale;
    }

    public void CheckDirection(float direction)
    {
        if (IsFlippable)
        {
            // Checks direction and determines if the sprite should flip or not
            if (direction >= transform.position.x && !FacingRight)
            {
                scale.x = -scale.x;
                FacingRight = true;
            }
            else if (direction <= transform.position.x && FacingRight)
            {
                scale.x = -scale.x;
                FacingRight = false;
            }
            transform.localScale = scale;
        }
    }
}
