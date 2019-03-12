using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Plastic/Recycle Code", fileName = "RecycleCode")]
public class RecycleCode : ScriptableObject
{
    public int RecyclingNumber;
    public Sprite Symbol;
    public string Abbreviation;
    public string PolymerName;
    [Multiline] public string Description;
}
