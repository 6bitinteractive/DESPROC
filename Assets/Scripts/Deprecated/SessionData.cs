using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deprecated
{
    //[CreateAssetMenu]
    public class SessionData : ScriptableObject
    {
        [SerializeField] private float baseTortgold;
        [SerializeField] private float baseTortpoints;
        [SerializeField] private int baseInventoryCapacity;
        [SerializeField] private float baseMovementSpeed;
        [SerializeField] private float basePickupSpeed;
        [SerializeField] private float baseLuck;

        [HideInInspector] public float Tortgold;
        [HideInInspector] public float Tortpoints;
        [HideInInspector] public int InventoryCapacity;
        [HideInInspector] public float MovementSpeed;
        [HideInInspector] public float PickupSpeed;
        [HideInInspector] public float Luck;
        [HideInInspector] public List<Plastic> Inventory;
        [HideInInspector] public List<Plastic> Bin;
        [HideInInspector] public int TotalTrash;
        [HideInInspector] public List<Quests> Quests;

        private void OnEnable()
        {
            Tortgold = baseTortgold;
            Tortpoints = baseTortpoints;
            InventoryCapacity = baseInventoryCapacity;
            MovementSpeed = baseMovementSpeed;
            PickupSpeed = basePickupSpeed;
            Luck = baseLuck;
            Inventory = new List<Plastic>();
            Bin = new List<Plastic>();
            TotalTrash = 0;
            Quests = new List<Quests>();
        }
    }
}