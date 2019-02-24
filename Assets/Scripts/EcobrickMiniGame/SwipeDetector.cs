using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    [SerializeField] private float minimumHorizontalSwipeDistance = 20f;
    [SerializeField] private float minimumVerticalSwipeDistance = 20f;

    private Vector2 startPosition;
    private Vector2 endPosition;
    private Vector2 DeltaSwipe { get { return startPosition - endPosition; } }

    public static event Action<SwipeData> OnSwipe = delegate { };

    private void Update()
    {
        #region Standalone Inputs
#if UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0))
        {
            SetStartPosition(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            SetEndPosition(Input.mousePosition);
            BroadcastSwipe();
        }
#endif
        #endregion

        #region Mobile Inputs
#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    SetStartPosition(touch.position);
                    break;

                case TouchPhase.Ended:
                    SetEndPosition(touch.position);
                    BroadcastSwipe();
                    break;
            }
        }
#endif
        #endregion
    }

    private void SetStartPosition(Vector2 position)
    {
        endPosition = startPosition = position;
    }

    private void SetEndPosition(Vector2 position)
    {
        endPosition = position;
    }

    private SwipeDirection DetermineSwipeDirection()
    {
        SwipeDirection swipeDirection = SwipeDirection.None;

        bool minimumDistanceMet = Mathf.Abs(DeltaSwipe.x) > minimumHorizontalSwipeDistance || Mathf.Abs(DeltaSwipe.y) > minimumVerticalSwipeDistance;
        bool isVerticalSwipe = Mathf.Abs(DeltaSwipe.y) > Mathf.Abs(DeltaSwipe.x);

        if (minimumDistanceMet)
        {
            if (isVerticalSwipe)
                swipeDirection |= (DeltaSwipe.y < 0) ? SwipeDirection.Up : SwipeDirection.Down;
            else
                swipeDirection |= (DeltaSwipe.x < 0) ? SwipeDirection.Right : SwipeDirection.Left;
        }

        return swipeDirection;
    }

    private void BroadcastSwipe()
    {
        SwipeData swipeData = new SwipeData()
        {
            StartPosition = startPosition,
            EndPosition = endPosition,
            Direction = DetermineSwipeDirection()
        };

        OnSwipe(swipeData);
    }
}

public enum SwipeDirection
{ // Bit flags
    None = 0,
    Left = 1,
    Right = 2,
    Up = 4,
    Down = 8,

    // Diagonal
    //LeftUp = 5,
    //RightUp = 6,
    //LeftDown = 9,
    //RightDown = 10
}

public struct SwipeData
{
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public SwipeDirection Direction;
}

// Reference:
// https://www.youtube.com/watch?v=poeXGuQ7eUo
// https://www.youtube.com/watch?v=jbFYYbu5bdc
// |= operator https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/or-assignment-operator
