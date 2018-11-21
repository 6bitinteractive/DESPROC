using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerData", fileName = "PlayerData")]
public class PlayerData : Data
{
    [SerializeField] protected float BaseTortgold;
    [SerializeField] protected float BaseTortpoints;
    [SerializeField] protected int BaseInventoryCapacity;
    [SerializeField] protected float BaseMovementSpeed;
    [SerializeField] protected float BasePickupSpeed;
    [SerializeField] protected float BaseLuck;

    [HideInInspector] public float Tortgold;
    [HideInInspector] public float Tortpoints;
    [HideInInspector] public int InventoryCapacity;
    [HideInInspector] public float MovementSpeed;
    [HideInInspector] public float PickupSpeed;
    [HideInInspector] public float Luck;
    [HideInInspector] public List<Plastic> Inventory;
    [HideInInspector] public List<Plastic> Bin;
    [HideInInspector] public string PreviousScene;
    [HideInInspector] public Vector2 PreviousPosition;

    protected void OnEnable()
    {
        Debug.Log("Player Data scriptable object has been reset.");

        Tortgold = BaseTortgold;
        Tortpoints = BaseTortpoints;
        InventoryCapacity = BaseInventoryCapacity;
        MovementSpeed = BaseMovementSpeed;
        PickupSpeed = BasePickupSpeed;
        Luck = BaseLuck;
        PreviousScene = string.Empty;
        PreviousPosition = Vector2.zero;

        if (Inventory != null)
            Inventory.Clear();
        else
            Inventory = new List<Plastic>();

        if (Bin != null)
            Bin.Clear();
        else
            Bin = new List<Plastic>();
    }
}
