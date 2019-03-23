using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockade : MonoBehaviour
{
    public PlasticData[] Plastics;
    public Transform SpawnLocation;

    // Use this for initialization
    void Start()
    {
        Shuffle.ShuffleArray(Plastics);
    }

}
