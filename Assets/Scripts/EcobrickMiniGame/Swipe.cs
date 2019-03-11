using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Swipe : ScriptableObject
{
    public Sprite Sprite;
    public SwipeDirection Direction;
}

[CreateAssetMenu]
public class SwipeSet : ScriptableObject
{
    public List<Swipe> Swipes;
}