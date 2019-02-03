using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataResetter : MonoBehaviour
{
    // All of the scriptable object assets that should be reset at the start of the game.
    public ResettableScriptableObject[] resettableScriptableObjects;

    private void Awake()
    {
        for (int i = 0; i < resettableScriptableObjects.Length; i++)
            resettableScriptableObjects[i].Reset();
    }
}
