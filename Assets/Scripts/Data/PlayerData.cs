﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Data", fileName = "PlayerData")]
public class PlayerData : Data
{
    public float Tortgold;
    public float Tortpoints;
    public int InventoryCapacity;
    public float MovementSpeed;
    public float PickupSpeed;
    public float Luck;
}
