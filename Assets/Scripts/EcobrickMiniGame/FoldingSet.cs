using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ecobrick Minigame/Folding Set")]

// The set of folds to be done per plastic
public class FoldingSet : ScriptableObject
{
    public static int MaxCount = 3;
    public Fold[] Folds = new Fold[MaxCount];
}
