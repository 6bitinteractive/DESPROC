using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class PlayerData : Data
{
    public float Tortgold = 0f;
    public float Tortpoints = 0f;
    public int InventoryCapacity = 9;
    public float MovementSpeed = 2.5f;
    public float PickupSpeed = 0f;
    public float Luck = 0f;
}
