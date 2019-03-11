using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SwipeDetector))]

public class EcobrickLevel : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private int foldsPerPlastic = 3;
    [SerializeField] private int plasticsPerBottle = 5;

    [Tooltip("The number of plastics to do before showing the packing-with-rod animation")]
    [SerializeField] private int plasticsDoneBeforePackWithRod = 3;
    [SerializeField] private int plasticsAvailable; // For testing only; value should be from player's saved data
    [SerializeField] private List<SwipeSet> swipeSets;

    [Header("Visual")]
    [SerializeField] private SpriteRenderer visualDirectionPrompt;
    [SerializeField] private Animator animator;

    //private SwipeDirection[] swipeDirectionPromptList;
    private SwipeSet[] swipeDirectionPrompts;
    private SwipeDirection[] swipeDirectionFlags;
    private SwipeDetector swipeDetector;

    private int ecobrickCount;
    private int currentSwipeSetIndex;
    private int currentSwipeIndex;
    private int currentTurnInBottle; // Keep track how many folds have been done per bottle; this is for tracking when to show the rod animation
    //private int turnsBeforeShowingRodAnim;
    private int turnsBeforeStartingNewBottle;
    private bool startNewBottle;
    private SwipeSet currentSwipeSet;
    private Swipe currentSwipeData;

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
        // Calculate how many turns/folds to do before showing the packing-with-rod animation
        //turnsBeforeShowingRodAnim = plasticsDoneBeforePackWithRod * foldsPerPlastic;

        // Calculate how many turns/folds before starting new bottle
        turnsBeforeStartingNewBottle = foldsPerPlastic * plasticsPerBottle;

        // Calculate the total number of bottles that can done given the number of plastics
        int bottlesThatCanBeFilled = (int)(plasticsAvailable / plasticsPerBottle);

        if (bottlesThatCanBeFilled > 0)
        {
            // Calculate how many total prompts will be needed
            int promptsRequired = foldsPerPlastic * plasticsPerBottle * bottlesThatCanBeFilled;

            Debug.Log("Bottles that can be filled: " + bottlesThatCanBeFilled);
            Debug.Log("Total folds/prompts the player will go through: " + promptsRequired);

            /*
            // Create array of prompts
            //swipeDirectionPromptList = new SwipeDirection[promptsRequired];
            //swipeDirectionFlags = new SwipeDirection[4];
            //swipeDirectionFlags[0] = SwipeDirection.Left;
            //swipeDirectionFlags[1] = SwipeDirection.Right;
            //swipeDirectionFlags[2] = SwipeDirection.Up;
            //swipeDirectionFlags[3] = SwipeDirection.Down;

            //for (int i = 0; i < swipeDirectionPromptList.Length; i++)
            //{
            //    int randomFlagIndex = Random.Range(0, swipeDirectionFlags.Length);
            //    swipeDirectionPromptList[i] = swipeDirectionFlags[randomFlagIndex];
            //}

            // TODO: Start new bottle

            // Start first prompt
            //Prompt(swipeDirectionPromptList[currentSwipeIndex]);
            */

            // Create array of prompts
            swipeDirectionPrompts = new SwipeSet[bottlesThatCanBeFilled];

            for (int i = 0; i < swipeDirectionPrompts.Length; i++)
            {
                int randomIndex = Random.Range(0, swipeSets.Count);
                swipeDirectionPrompts[i] = swipeSets[randomIndex];
            }

            currentSwipeSet = swipeDirectionPrompts[currentSwipeSetIndex];
            currentSwipeData = currentSwipeSet.Swipes[currentSwipeIndex];
            StartCoroutine(Prompt(currentSwipeData.Direction));
        }
        else
        {
            Debug.Log("Not enough plastics to fill a bottle.");
        }
    }

    private void StartNewBottle()
    {
        // TODO: Add animation of new bottle to fill
    }

    private IEnumerator Prompt(SwipeDirection direction)
    {
        // Enable SwipeDetector
        swipeDetector.enabled = true;

        // Change visuals
        visualDirectionPrompt.sprite = currentSwipeData.Sprite;

        // If it's the first (i.e no direction)
        if (currentSwipeData.Direction == SwipeDirection.None)
        {
            currentSwipeIndex++;
            currentSwipeData = currentSwipeSet.Swipes[currentSwipeIndex];
            yield return new WaitForSeconds(0.3f);
            Prompt(currentSwipeData.Direction);
        }

        // TODO: Add visual prompt

        Debug.Log("Swipe to: " + direction);
        yield return null;
    }

    private void VerifySwipe(SwipeData data)
    {
        Debug.Log("Player Swiped: " + data.Direction);

        // If player swiped correctly
        //if (swipeDirectionPromptList[currentSwipeIndex] == data.Direction)
        if (currentSwipeData.Direction == data.Direction)
        {
            Debug.Log("Player swiped correctly.");

            // If we've reached the end of the current set
            if (currentSwipeIndex >= currentSwipeSet.Swipes.Count)
            {
                currentSwipeSetIndex++; // Move to the next set
                currentSwipeSet = swipeDirectionPrompts[currentSwipeSetIndex];
                startNewBottle = true;
            }
            else // Move index to the next direction to prompt
            {
                currentSwipeIndex++;
                currentSwipeData = currentSwipeSet.Swipes[currentSwipeIndex];
            }

            // +1 to current turn
            currentTurnInBottle++;

            // Disable SwipeDetector once player swipes in the correct direction
            swipeDetector.enabled = false;

            // TODO: Add animation of placing plastic in bottle; add visual feedback for correct swipe
        }
        else
        {
            // Do nothing; the currentSwipeIndex remains the same
            Debug.Log("Player swiped incorrectly.");

            // TODO: Add visual feedback for incorrect swipe
        }

        // Show packing-with-rod animation
        //if (currentTurnInBottle % turnsBeforeShowingRodAnim == 0)
        //{
        //    // TODO: Wait for player tap
        //    // TODO: Show packing-with-rod animation
        //    Debug.Log("Current index: " + currentSwipeIndex + " | Show packing with rod animation.");
        //}

        // If it's time to start a new bottle
        //if (currentSwipeSetIndex % turnsBeforeStartingNewBottle == 0)
        if (startNewBottle)
        {
            startNewBottle = false;
            // TODO: Show packing-with-rod animation
            Debug.Log("Show packing with rod animation; completed an ecobrick.");
            animator.SetBool("PoundStick", true);

            // Increase ecobrick count
            ecobrickCount++;
            Debug.Log("Ecobrick done: " + ecobrickCount);

            // Reset turn counter
            currentTurnInBottle = 0;

            // TODO: Start new bottle
        }

        // If we've reached the end of creating all the ecobricks
        //if (currentSwipeIndex >= swipeDirectionPromptList.Length)
        if (currentSwipeSetIndex >= swipeDirectionPrompts.Length)
        {
            Debug.Log("Minigame End");
            swipeDetector.enabled = false;

            // TODO: Add panel overlay telling the player that they'll be brought back to the main island
            // TODO: Save number of ecobricks done
            OnGameEndReached.Invoke();

            return;
        }

        //Prompt(swipeDirectionPromptList[currentSwipeIndex]);
        Prompt(currentSwipeData.Direction);
    }
}
