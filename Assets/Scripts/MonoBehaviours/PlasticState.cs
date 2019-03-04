using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlasticInteractable))]

public class PlasticState : MonoBehaviour
{
    public bool IsSorted { get; set; }
    public RecycleCode PlasticType { get; private set; }

    private PlasticInteractable plastic;

    private void Start()
    {
        plastic = GetComponent<PlasticInteractable>();
        PlasticType = plastic.GetPlasticData().RecycleCode;
        IsSorted = false;
    }
}
