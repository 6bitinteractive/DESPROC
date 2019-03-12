using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SwipeDetector))]

public class EcobrickLevel : MonoBehaviour
{
    #region Public/Serialized fields
    [Header("Test")]
    [SerializeField] private int plasticCount = 13;

    [Space]

    [Header("Setup")]
    [SerializeField] private TurtleTale.SessionData sessionData;
    [SerializeField] private int plasticsPerBottle = 5;
    [SerializeField] private FoldingSet[] foldingSets;

    [Space]

    [Header("UI")]
    [SerializeField] private float lastFoldDisplayDelay = 1.3f;
    [SerializeField] private float promptDelay = 0.5f;
    [SerializeField] private SpriteRenderer promptDisplay;
    [SerializeField] private GameObject directionPanel;
    [SerializeField] private Image directionImage;
    [SerializeField] private Text directionText;
    [SerializeField] private Text ecobrickCountText;
    [SerializeField] private Text ecobrickTotalCountText;
    [SerializeField] private Animator bottleAnimator;
    [SerializeField] private Animator stickAnimator;

    [Space]

    [Header("UI | Direction Sprites (Order: Left - Right - Up - Down")]
    [SerializeField] private Sprite[] directionSprites = new Sprite[directionCount];
    private static int directionCount = 4;

    [Space]

    public UnityEvent OnGameEnd = new UnityEvent();
    public UnityEvent OnAskTap = new UnityEvent();
    public UnityEvent OnTap = new UnityEvent();

    #endregion

    #region Private fields
    private SwipeDetector swipeDetector;
    //private SwipeDirection[] swipeDirectionFlags;
    private List<FoldingSet> prompts = new List<FoldingSet>(); // Ratio: 1 plastic = 1 folding set

    private int ecobrickCount;
    private int currentFold;
    private int currentFoldSet;
    private SwipeDirection currentDirection;
    #endregion

    private void Awake()
    {
        swipeDetector = GetComponent<SwipeDetector>();
        swipeDetector.enabled = false;

        // Setup list of directions
        //swipeDirectionFlags = new SwipeDirection[directionCount];
        //swipeDirectionFlags[0] = SwipeDirection.Left;
        //swipeDirectionFlags[1] = SwipeDirection.Right;
        //swipeDirectionFlags[2] = SwipeDirection.Up;
        //swipeDirectionFlags[3] = SwipeDirection.Down;
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
        // Hide folding prompt
        HidePrompt();

        // Determine how many bottles can be filled
        //int bottlesThatCanBeFilled = (int)(sessionData.PickedUpPlastic.Count / plasticsPerBottle);
        int bottlesThatCanBeFilled = (int)(plasticCount / plasticsPerBottle); // TEST
        Debug.Log("Bottles that can be filled: " + bottlesThatCanBeFilled);


        if (bottlesThatCanBeFilled > 0)
        {
            // Determine how many plastics will be used
            int plasticNeeded = plasticsPerBottle * bottlesThatCanBeFilled;
            //Debug.Log("Number of plastics that will be used: " + plasticNeeded + "/" + sessionData.PickedUpPlastic.Count);
            Debug.Log("Number of plastics that will be used: " + plasticNeeded + "/" + plasticCount); // TEST


            // Setup the prompts based on how many plastics will be used
            for (int i = 0; i < plasticNeeded; i++)
            {
                int randomFoldingSetIndex = Random.Range(0, foldingSets.Length);
                prompts.Add(foldingSets[randomFoldingSetIndex]);
            }

            // Update count display
            UpdateCountDisplay();

            // Turn on bottle animation
            bottleAnimator.SetBool("StartGame", true);
            StartCoroutine(StartNewBottle());
        }
        else
        {
            Debug.Log("Not enough plastics to fill a bottle.");
            OnGameEnd.Invoke();
        }
    }

    private void UpdateCountDisplay()
    {
        ecobrickCountText.text = ecobrickCount.ToString();
        ecobrickTotalCountText.text = string.Format("TOTAL {0}", sessionData.EcobricksDone.ToString());
    }

    private IEnumerator StartNewBottle()
    {
        Debug.Log("Start new bottle");
        HidePrompt();
        swipeDetector.enabled = false;

        // Make sure the bottle doesn't play the slide-out animation
        bottleAnimator.SetBool("StartNewBottle", false);

        // Wait until we enter current state
        yield return new WaitUntil(() => bottleAnimator.GetCurrentAnimatorStateInfo(0).IsName("SlideInBottle"));

        // Wait until done playing
        yield return new WaitUntil(() => IsDonePlaying(bottleAnimator));

        yield return new WaitForSeconds(promptDelay); // Delay showing of prompt
        ShowPrompt();
    }

    private void ShowPrompt()
    {
        // Set sprite
        //if (currentFoldSet < curre)
        promptDisplay.sprite = prompts[currentFoldSet].Folds[currentFold].Sprite;

        // Display
        promptDisplay.enabled = true;

        // If it's not the first fold of the set nor is it beyond the set's index range
        if ((currentFold + 1) < FoldingSet.MaxCount)
        {
            // Set direction
            currentDirection = GetDirectionPrompt();
            directionImage.sprite = GetDirectionSprite(currentDirection);
            directionText.text = string.Format("Swipe " + currentDirection.ToString());
            directionPanel.SetActive(true);

            // Enable SwipeDetector
            swipeDetector.enabled = true;
        }
        else
        {
            directionPanel.SetActive(false);
            StartCoroutine(MoveForward());
        }
    }

    private SwipeDirection GetDirectionPrompt()
    {
        // We get the direction of the next fold
        return prompts[currentFoldSet].Folds[currentFold + 1].SwipeDirection;
    }

    private Sprite GetDirectionSprite(SwipeDirection direction)
    {
        switch (direction)
        {
            case SwipeDirection.Left:
                return directionSprites[0];
            case SwipeDirection.Right:
                return directionSprites[1];
            case SwipeDirection.Up:
                return directionSprites[2];
            case SwipeDirection.Down:
                return directionSprites[3];
            default:
                return null;
        }
    }

    private void VerifySwipe(SwipeData swipeData)
    {
        // If correct +1 currentFold
        // If currentFold >= FoldSet.MaxtCount => start new bottle

        Debug.Log("Player Swiped: " + swipeData.Direction);
        swipeDetector.enabled = false;

        // TODO: Add feedback when correct/wrong

        if (swipeData.Direction == currentDirection)
        {
            StartCoroutine(MoveForward());
        }
        else
        {
            Debug.Log("Player swiped incorrectly.");
            ShowPrompt();
        }
    }

    private IEnumerator MoveForward()
    {
        // Move to the next fold
        currentFold++;
        Debug.Log(currentFold);

        // If we've reached the end of the current set
        if (currentFold >= FoldingSet.MaxCount)
        {
            // Reset current fold count
            currentFold = 0;

            // Move to next fold set
            currentFoldSet++;

            Debug.Log("Move to next fold set");
            yield return StartCoroutine(PlayStickAnimation());

            // Finished an ecobrick
            if ((currentFoldSet >= plasticsPerBottle - 1) && (currentFoldSet % (plasticsPerBottle - 1) == 0))
            {
                // +1 Ecobrick Count
                ecobrickCount++;
                sessionData.EcobricksDone++;
                UpdateCountDisplay();

                // If we've reached the end
                if (currentFoldSet >= prompts.Count - 1)
                {
                    OnEnd();
                    yield break;
                }

                // Start a new bottle
                bottleAnimator.SetBool("StartNewBottle", true);

                // Wait until we enter current state
                yield return new WaitUntil(() => bottleAnimator.GetCurrentAnimatorStateInfo(0).IsName("SlideOutBottle"));

                // Wait until done playing
                yield return new WaitUntil(() => IsDonePlaying(bottleAnimator));

                StartCoroutine(StartNewBottle());
            }
            else
            {
                ShowPrompt();
            }
        }
        else
        {
            ShowPrompt();
        }
    }

    private IEnumerator PlayStickAnimation()
    {
        // Wait a bit befor hiding last fold
        yield return new WaitForSeconds(lastFoldDisplayDelay);
        HidePrompt();

        int tap = 0;
        while (tap != 3)
        {
            OnAskTap.Invoke();
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            tap++;
            OnTap.Invoke();
            Debug.Log("Tap Count: " + tap);

            // Play stick animation
            stickAnimator.SetBool("PoundStick", true);

            // Wait until we enter current state
            yield return new WaitUntil(() => stickAnimator.GetCurrentAnimatorStateInfo(0).IsName("PoundStick"));

            // Wait for the animation
            yield return new WaitUntil(() => IsDonePlaying(stickAnimator));
            Debug.Log("Done playing stick animation: " + IsDonePlaying(stickAnimator));
            stickAnimator.SetBool("PoundStick", false);
        }

    }

    private void OnEnd()
    {
        Debug.Log("Minigame End");
        HidePrompt();
        swipeDetector.enabled = false;

        // Turn off animation
        bottleAnimator.SetBool("StartGame", false);
        stickAnimator.SetBool("PoundStick", false);

        // Remove plastics used from sessionData list
        int totalPlasticUsed = ecobrickCount * plasticsPerBottle;
        //sessionData.PickedUpPlastic.RemoveRange(0, totalPlasticUsed);

        OnGameEnd.Invoke();
    }

    private bool IsDonePlaying(Animator animator)
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
    }

    private void HidePrompt()
    {
        promptDisplay.enabled = false;
        directionPanel.SetActive(false);
    }
}
