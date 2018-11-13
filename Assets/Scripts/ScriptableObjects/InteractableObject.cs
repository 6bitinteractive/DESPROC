using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : ScriptableObject
{
    [Header("Display")]
    public Sprite Sprite;

    [Header("Stats")]
    public int Value = 1;
}
