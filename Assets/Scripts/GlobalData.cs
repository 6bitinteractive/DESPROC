using UnityEngine;
using System.Collections;

public class GlobalData : MonoBehaviour
{
    public static GlobalData Instance { get; private set; }

    public float Tortgold = 0;
    public float Tortpoints = 0;

    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
    }
}

