using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCPrompt : MonoBehaviour
{
    public GameEvent OnNPCCollision;
    public GameEvent OnNPCCollisionExit;
    // The commented lines will be uncommented once we start to implement it to every scene
   // [SerializeField] Image TalkSymbol;
  //  [SerializeField] Canvas canvas;

    private void Awake()
    {
      //  canvas.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If colliding with player
        if (collision.gameObject.layer == 8)
        {
           // canvas.enabled = true;
            OnNPCCollision.Raise();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If colliding with player
        if (collision.gameObject.layer == 8)
        {
           // canvas.enabled = false;
            OnNPCCollisionExit.Raise();
        }
    }
}
