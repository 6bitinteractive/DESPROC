using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationHelper : MonoBehaviour
{
    public UnityEvent OnAnimEnded = new UnityEvent();

    public void EndAnimation()
    {
        OnAnimEnded.Invoke();
    }
}
