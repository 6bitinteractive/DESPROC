using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager : MonoBehaviour
{
    public static int Total { get; private set; }
    private PlasticInteractable[] Trash;

    private void Awake()
    {
        int Total = transform.childCount;
        Trash = GetComponentsInChildren<PlasticInteractable>();
    }
}
