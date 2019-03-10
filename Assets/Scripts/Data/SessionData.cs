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

        [Header("DO NOT CHANGE THE VALUES; Exposed for testing")]
        public float Tortgold;
        public float Tortpoints;
        public int InventoryCapacity;
        public float MovementSpeed;
        public float PickupSpeed;
        public float Luck;
        public List<Plastic> Inventory;
        public List<Plastic> Bin;
        public int TotalPickedUp;
        public List<GameObject> PickedUpPlastic;
        public List<GameObject> ForEcobrick;
        public int EcobricksDone;
        public int TurtleEgg;
        public int Happiness;
        public float SortingBestTime;
        public List<Quests> Quests;
        //public AudioClip audioToPersist;
        //public float audioTimePause;

        private void OnEnable()
        {
            Reset();
            //Debug.Log("Inventory Count: " + Inventory.Count);
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
            TotalPickedUp = 0;
            PickedUpPlastic = new List<GameObject>();
            ForEcobrick = new List<GameObject>();
            EcobricksDone = 0;
            TurtleEgg = 0;
            Happiness = 0;
            SortingBestTime = 0f;
            Quests = new List<Quests>();
        }
    }
}