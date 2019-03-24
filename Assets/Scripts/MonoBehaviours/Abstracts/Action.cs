using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Action : MonoBehaviour
{
    [SerializeField] protected KeyCode Key;
    [SerializeField] protected LayerMask interactableLayerMask;

    protected GameObject target;

    public abstract void Act();

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
        //Debug.Log("Target: " + target);
        #region Standard Input
#if UNITY_STANDALONE_WIN
        if (Input.GetKeyDown(Key))
        {
            Act();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(inputPosition, Vector2.zero, Mathf.Infinity, 1 << interactableLayerMask);

            if (hit.transform != null)
            {
                Act();
            }
        }
#endif
        #endregion

        #region Mobile Input
#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 inputPosition = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(inputPosition, Vector2.zero, Mathf.Infinity, 1 << interactableLayerMask);

                if (hit.transform != null)
                {
                    Act();
                }
            }
        }
#endif
        #endregion
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if ((interactableLayerMask.value & 1 << collision.gameObject.layer) != 0)
        {
            target = collision.gameObject;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        target = null;
    }
}

// Reference: Check layer mask for collision
// http://answers.unity.com/answers/454913/view.html
// https://gamedev.stackexchange.com/questions/119667/how-to-get-the-gameobjects-layermask
