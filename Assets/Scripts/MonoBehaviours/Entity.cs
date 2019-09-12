using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public EntityRuntimeSet EntityRunTimeSet;

    private void OnEnable()
    {
        EntityRunTimeSet.Add(this);
    }

    private void OnDisable()
    {
        EntityRunTimeSet.Remove(this);
    }
}
