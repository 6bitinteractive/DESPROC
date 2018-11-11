using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class PlayerData : Data
{
    public int Gold;
    public int Points;
    public int InventoryCapacity;
    public float MovementSpeed;
    public float PickupSpeed;
    public float Luck;
}
