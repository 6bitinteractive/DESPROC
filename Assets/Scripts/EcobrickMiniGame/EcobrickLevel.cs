using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SwipeDetector))]

public class EcobrickLevel : MonoBehaviour
{
    [SerializeField] private int plasticsPerBottle = 5;
    [SerializeField] private int plasticsAvailable; // For testing only; value should be from player's saved data

    private SwipeDirection[] swipeDirections;
    private int[] swipeDirectionFlags;

    private int currentSwipeIndex;
    private SwipeDetector swipeDetector;

    public UnityEvent OnGameEndReached = new UnityEvent();

    private void Awake()
    {
        swipeDetector = GetComponent<SwipeDetector>();
        swipeDetector.enabled = false;
    }

    private void OnEnable()
    {
        SwipeDetector.OnSwipe += VerifySwipe;
    }

    private void OnDisable()
    {
        SwipeDetector.OnSwipe -= VerifySwipe;
    }

    private void Start()
    {
        // Figure out how many plastics will be used in the minigame
        int bottlesThatCanBeFilled = (int)(plasticsAvailable / plasticsPerBottle);
        int plasticsRequired = 0;

        if (bottlesThatCanBeFilled > 0)
        {
            plasticsRequired = plasticsPerBottle * bottlesThatCanBeFilled;

            Debug.Log("Bottles that can be filled: " + bottlesThatCanBeFilled);
            Debug.Log("Plastics to be used for this minigame: " + plasticsRequired);

            // Create array
            // For testing only
            swipeDirections = new SwipeDirection[plasticsRequired];
            swipeDirectionFlags = new int[4];
            swipeDirectionFlags[0] = 1;
            swipeDirectionFlags[1] = 2;
            swipeDirectionFlags[2] = 4;
            swipeDirectionFlags[3] = 8;

            for (int i = 0; i < swipeDirections.Length; i++)
            {
                int randomFlagIndex = Random.Range(0, swipeDirectionFlags.Length);
                swipeDirections[i] = (SwipeDirection)swipeDirectionFlags[randomFlagIndex];
            }

            // Start first prompt
            Prompt(swipeDirections[currentSwipeIndex]);
        }
        else
        {
            Debug.Log("Not enough plastics to fill a bottle.");
        }
    }

    private void Prompt(SwipeDirection direction)
    {
        // Enable SwipeDetector
        swipeDetector.enabled = true;

        Debug.Log("Swipe to: " + direction);
    }

    private void VerifySwipe(SwipeData data)
    {
        Debug.Log("Player Swiped: " + data.Direction);

        // If player swiped correctly
        if (swipeDirections[currentSwipeIndex] == data.Direction)
        {
            Debug.Log("Player swiped correctly.");

            // Prompt for the next direction
            currentSwipeIndex++;

            // Disable SwipeDetector once player swipes in the correct direction
            swipeDetector.enabled = false;

            // TODO: Do animation, visual feedback
        }
        else
        {
            Debug.Log("Player swiped incorrectly.");
        }

        // If we've reached the end
        if (currentSwipeIndex >= swipeDirections.Length)
        {
            Debug.Log("Minigame End");
            swipeDetector.enabled = false;

            OnGameEndReached.Invoke();

            return;
        }

        Prompt(swipeDirections[currentSwipeIndex]);
    }
}
