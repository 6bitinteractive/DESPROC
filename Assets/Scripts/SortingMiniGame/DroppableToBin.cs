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
#if UNITY_STANDALONE_WIN
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#endif
        #endregion

        #region Mobile Input
#if UNITY_ANDROID || UNITY_IOS

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sortingBin = collision.GetComponent<Sorting.SortingBin>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sortingBin = null;
    }
}
