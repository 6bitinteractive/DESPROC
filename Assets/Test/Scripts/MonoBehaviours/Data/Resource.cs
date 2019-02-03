using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public int Value { get; set; }

    public void Add(int value)
    {
        Value += value;
    }
}
