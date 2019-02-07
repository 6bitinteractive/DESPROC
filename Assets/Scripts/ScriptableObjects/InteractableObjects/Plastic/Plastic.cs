﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Plastic/Plastic", fileName = "Plastic")]
public class Plastic : InteractableObject
{
    public RecycleCode RecycleCode;
    public AudioClip PickupSfx;
    public int StackSize = 15;
}
