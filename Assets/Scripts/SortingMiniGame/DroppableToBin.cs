using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppableToBin : MonoBehaviour
{
    private bool grabbed;
    private Sorting.SortingBin sortingBin;

    private void Update()
    {
        if (grabbed)
        {
            Drag();
        }
    }

    private void Drag()
    {
        #region Standard Input
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f); // set z-position to 0 to avoid it going "invisible" (being set to -10)
#endif
        #endregion

        #region Mobile Input
#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                case TouchPhase.Moved:
                    transform.position = new Vector3(touchPosition.x, touchPosition.y, 0f);
                    break;
            }
        }
#endif
        #endregion
    }

    private void OnMouseDown()
    {
        grabbed = true;
    }

    private void OnMouseUp()
    {
        grabbed = false;

        // Drop to bin
        if (sortingBin != null)
        {
            Debug.Log("Dropped " + gameObject.name + " to " + sortingBin.name);
            sortingBin.OnDropToBin.Invoke(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        sortingBin = collision.GetComponent<Sorting.SortingBin>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sortingBin = null;
    }
}
