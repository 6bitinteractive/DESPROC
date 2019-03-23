using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleTale
{
    [CreateAssetMenu(menuName = "Persistent Data/Session Data", fileName = "SessionData")]
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
        public List<PlasticData> Inventory;
        public List<PlasticData> Bin;
        public int TotalPickedUp;
        public List<GameObject> CollectedPlastic;
        public List<GameObject> SortedPlastic;
        public int EcobricksDone;
        public int TurtleEgg;
        public int Happiness;
        public float SortingBestTime;
        public List<Quests> Quests;
        public List<QuestGiverData> QuestGiverDatas;

        private void OnEnable()
        {
            Reset();
        }

        public void Reset()
        {
            Tortgold = baseTortgold;
            Tortpoints = baseTortpoints;
            InventoryCapacity = baseInventoryCapacity;
            MovementSpeed = baseMovementSpeed;
            PickupSpeed = basePickupSpeed;
            Luck = baseLuck;
            Inventory = new List<PlasticData>();
            Bin = new List<PlasticData>();
            TotalPickedUp = 0;
            CollectedPlastic = new List<GameObject>();
            SortedPlastic = new List<GameObject>();
            EcobricksDone = 0;
            TurtleEgg = 0;
            Happiness = 0;
            SortingBestTime = 0f;
            Quests = new List<Quests>();
            QuestGiverDatas = new List<QuestGiverData>();
        }
    }
}