using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneDataManager : MonoBehaviour
{
    [SerializeField] protected PlayerDataHandler playerDataHandler;

    protected virtual void Start()
    {
        
    }
}
