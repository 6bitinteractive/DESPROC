﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class PlayerData : Data
{
    public float Tortgold;
    public float Tortpoints;
    public int InventoryCapacity;
    public float MovementSpeed;
    public float PickupSpeed;
    public float Luck;
    public List<PlasticData> Inventory;
    public List<PlasticData> Bin;
    public int TotalTrash;

    public PlayerData()
    {
        Tortgold = 0f;
        Tortpoints = 0f;
        InventoryCapacity = 9;
        MovementSpeed = 2.5f;
        PickupSpeed = 1f;
        Luck = 1f;
        Inventory = new List<PlasticData>();
        Bin = new List<PlasticData>();
        TotalTrash = 0;
    }
}
