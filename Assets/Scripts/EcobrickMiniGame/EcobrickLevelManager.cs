using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Deprecated
{
    [RequireComponent(typeof(SwipeDetector))]

    public class EcobrickLevelManager : MonoBehaviour
    {
        [SerializeField] private int foldsPerPlastic = 3;
        [SerializeField] private int plasticsPerBottle = 5;

        [Tooltip("The number of plastics to do before showing the packing-with-rod animation")]
        [SerializeField] private int plasticsDoneBeforePackWithRod = 3;

        [SerializeField] private int plasticsAvailable; // For testing only; value should be from player's saved data

        private SwipeDirection[] swipeDirectionPromptList;
        private SwipeDirection[] swipeDirectionFlags;
        private SwipeDetector swipeDetector;

        private int ecobrickCount;
        private int currentSwipeIndex;
        private int currentTurnInBottle; // Keep track how many folds have been done per bottle; this is for tracking when to show the rod animation
        private int turnsBeforeShowingRodAnim;
        private int turnsBeforeStartingNewBottle;

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
            turnsBeforeShowingRodAnim = plasticsDoneBeforePackWithRod * foldsPerPlastic;

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

                // Create array of prompts
                swipeDirectionPromptList = new SwipeDirection[promptsRequired];
                swipeDirectionFlags = new SwipeDirection[4];
                swipeDirectionFlags[0] = SwipeDirection.Left;
                swipeDirectionFlags[1] = SwipeDirection.Right;
                swipeDirectionFlags[2] = SwipeDirection.Up;
                swipeDirectionFlags[3] = SwipeDirection.Down;

                for (int i = 0; i < swipeDirectionPromptList.Length; i++)
                {
                    int randomFlagIndex = Random.Range(0, swipeDirectionFlags.Length);
                    swipeDirectionPromptList[i] = swipeDirectionFlags[randomFlagIndex];
                }

                // TODO: Start new bottle

                // Start first prompt
                Prompt(swipeDirectionPromptList[currentSwipeIndex]);
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

        private void Prompt(SwipeDirection direction)
        {
            // Enable SwipeDetector
            swipeDetector.enabled = true;

            // TODO: Add visual prompt

            Debug.Log("Swipe to: " + direction);
        }

        private void VerifySwipe(SwipeData data)
        {
            Debug.Log("Player Swiped: " + data.Direction);

            // If player swiped correctly
            if (swipeDirectionPromptList[currentSwipeIndex] == data.Direction)
            {
                Debug.Log("Player swiped correctly.");

                // Move index to the next direction to prompt
                currentSwipeIndex++;

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
            if (currentTurnInBottle % turnsBeforeShowingRodAnim == 0)
            {
                // TODO: Wait for player tap
                // TODO: Show packing-with-rod animation
                Debug.Log("Current index: " + currentSwipeIndex + " | Show packing with rod animation.");
            }

            // If it's time to start a new bottle
            if (currentSwipeIndex % turnsBeforeStartingNewBottle == 0)
            {
                // TODO: Show packing-with-rod animation
                Debug.Log("Show packing with rod animation; completed an ecobrick.");

                // Increase ecobrick count
                ecobrickCount++;
                Debug.Log("Ecobrick done: " + ecobrickCount);

                // Reset turn counter
                currentTurnInBottle = 0;

                // TODO: Start new bottle
            }

            // If we've reached the end of creating all the ecobricks
            if (currentSwipeIndex >= swipeDirectionPromptList.Length)
            {
                Debug.Log("Minigame End");
                swipeDetector.enabled = false;

                // TODO: Add panel overlay telling the player that they'll be brought back to the main island
                // TODO: Save number of ecobricks done
                OnGameEndReached.Invoke();

                return;
            }

            Prompt(swipeDirectionPromptList[currentSwipeIndex]);
        }
    }
}