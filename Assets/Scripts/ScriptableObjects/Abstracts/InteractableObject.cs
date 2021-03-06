﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : ScriptableObject
{
    [Header("Display")]
    public Sprite Sprite;

    [Header("Data")]
    public string Name;
}
