using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest : MonoBehaviour
{
    public abstract bool IsComplete();
    public abstract void Complete();
    public abstract void DrawHUD();
}
