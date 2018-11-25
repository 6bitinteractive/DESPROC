using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleTale
{
    [CreateAssetMenu(menuName = "Session Data", fileName = "SessionData")]
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
        [HideInInspector] public AudioClip audioToPersist;
        [HideInInspector] public float audioTimePause;

        private void OnEnable()
        {
            Reset();
            Debug.Log("Inventory Count: " + Inventory.Count);
        }

        public void Reset()
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
        }
    }
}