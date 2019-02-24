using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeLogger : MonoBehaviour
{
    private void OnEnable()
    {
        SwipeDetector.OnSwipe += LogSwipe;
    }

    private void OnDisable()
    {
        SwipeDetector.OnSwipe -= LogSwipe;
    }

    private void LogSwipe(SwipeData swipeData)
    {
        Debug.Log("Swipe Direction: " + swipeData.Direction);
    }
}
