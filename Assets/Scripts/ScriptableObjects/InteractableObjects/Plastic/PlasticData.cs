using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Plastic/PlasticData", fileName = "PlasticData")]
public class PlasticData : InteractableObject
{
    public RecycleCode RecycleCode;
    public AudioClip PickupSfx;
    public int StackSize = 15;
}
