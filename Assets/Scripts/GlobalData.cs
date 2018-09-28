using UnityEngine;
using System.Collections;

public class GlobalData : MonoBehaviour
{
    public static GlobalData Instance { get; private set; }

    public float Tortgold;
    public float Tortpoints;
    public int InventoryCapacity;
    public float MovementSpeed;
    public float PickupSpeed;
    public float Luck;
    public float Clock;

    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
    }
}

