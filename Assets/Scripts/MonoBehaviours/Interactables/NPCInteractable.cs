using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]

public class NPCInteractable : MonoBehaviour
{
    public UnityEvent OnNPCTalk = new UnityEvent();

    public void Talk()
    {
        OnNPCTalk.Invoke();
    }
}
