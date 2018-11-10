using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Gold = 0;
    public int Points = 0;
    public int InventoryCapacity = 20;
    public float MovementSpeed = 3f;
    public float PickupSpeed = 1.5f;
    public float Luck = 0f;
}
