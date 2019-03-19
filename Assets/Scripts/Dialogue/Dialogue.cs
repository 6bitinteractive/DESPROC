using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    // First element of the sentence array is default dialogue
    public string questName;
    public Sentence[] sentenceArray;
    public GameEvent dialogueEndTrigger;
}
