using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Instruction", fileName = "Instruction00")]

public class Instruction : ScriptableObject
{
    public string text;
    public string correctAnswer;
}
