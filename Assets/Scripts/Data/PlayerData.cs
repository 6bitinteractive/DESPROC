using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerData", fileName = "PlayerData")]
public class PlayerData : Data
{
    public float Tortgold;
    public float Tortpoints;
    public int InventoryCapacity;
    public float MovementSpeed;
    public float PickupSpeed;
    public float Luck;
    public Plastic[] Inventory;
    public Plastic[] Bin;
    public string PreviousScene;
    public Vector2 PreviousPosition;
}
