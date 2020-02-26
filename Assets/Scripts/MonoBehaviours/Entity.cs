using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public EntityRuntimeSet EntityRunTimeSetVariable;

    private void OnEnable()
    {
        if (EntityRunTimeSetVariable != null)
            EntityRunTimeSetVariable.Add(this);
        else
            Debug.Log("EntityRunTimeSetVariable is empty.");
    }

    private void OnDisable()
    {
        if (EntityRunTimeSetVariable != null)
            EntityRunTimeSetVariable.Remove(this);
        else
            Debug.Log("EntityRunTimeSetVariable is empty.");
    }
}
