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
    public AudioSource foldSFX;
    public AudioSource stickSFX;

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
    [SerializeField] private Text plasticLeftCountText;
    [SerializeField] private Animator bottleAnimator;
    [SerializeField] private Animator stickAnimator;
    [SerializeField] private ParticleSystem particles;

    [Space]

    [Header("UI | Direction Sprites (Order: Left - Right - Up - Down")]
    [SerializeField] private Sprite[] directionSprites = new Sprite[directionCount];
    private static int directionCount = 4;

    [Space]

    public UnityEvent OnNotEnoughPlastic = new UnityEvent();
    public UnityEvent OnGameEnd = new UnityEvent();
    public UnityEvent OnActionPrompt = new UnityEvent();
    public UnityEvent OnActionDone = new UnityEvent();

    #endregion

    #region Private fields
    private SwipeDetector swipeDetector;
    private List<FoldingSet> prompts = new List<FoldingSet>(); // Ratio: 1 plastic = 1 folding set; 1 folding set = FoldingSet.MaxCount

    private int ecobrickCount;
    private int currentFold;
    private int currentFoldSet;
    private int plasticLeft;
    private SwipeDirection currentDirection;
    private SwipeDirection playerSwipe;
    private bool playingStickAnimation;
    #endregion

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
        // Hide folding prompt
        HidePrompt();

        // Determine how many bottles can be filled
        //int bottlesThatCanBeFilled = (int)(plasticCount / plasticsPerBottle); // TEST
        //int bottlesThatCanBeFilled = (int)(sessionData.SortedPlastic.Count / plasticsPerBottle); // Original design: sorted plastics are used for ecobrick minigame
        int bottlesThatCanBeFilled = (int)(sessionData.CollectedPlastic.Count / plasticsPerBottle);
        Debug.Log("Bottles that can be filled: " + bottlesThatCanBeFilled);


        if (bottlesThatCanBeFilled > 0)
        {
            // Determine how many plastics will be used
            int plasticNeeded = plasticsPerBottle * bottlesThatCanBeFilled;
            //Debug.Log("Number of plastics that will be used: " + plasticNeeded + "/" + plasticCount); // TEST
            //Debug.Log("Number of plastics that will be used: " + plasticNeeded + "/" + sessionData.SortedPlastic.Count);
            Debug.Log("Number of plastics that will be used: " + plasticNeeded + "/" + sessionData.CollectedPlastic.Count);

            // Setup the prompts based on how many plastics will be used
            for (int i = 0; i < plasticNeeded; i++)
            {
                int randomFoldingSetIndex = Random.Range(0, foldingSets.Length);
                prompts.Add(foldingSets[randomFoldingSetIndex]);
            }

            // Update count display
            UpdateEcobrickCountDisplay();

            plasticLeft = plasticNeeded;
            UpdatePlasticLeftCountDisplay();

            // Turn on bottle animation
            bottleAnimator.SetBool("StartGame", true);
            StartCoroutine(StartNewBottle());
        }
        else
        {
            // Show some accurate data even if player will not continue with the minigame
            UpdateEcobrickCountDisplay();
            //plasticLeftCountText.text = sessionData.SortedPlastic.Count.ToString();
            plasticLeftCountText.text = sessionData.CollectedPlastic.Count.ToString();

            Debug.Log("Not enough plastics to fill a bottle.");
            OnNotEnoughPlastic.Invoke();
        }
    }

    private void UpdateEcobrickCountDisplay()
    {
        ecobrickCountText.text = ecobrickCount.ToString();
        //ecobrickTotalCountText.text = string.Format("TOTAL {0}", sessionData.EcobricksDone.ToString());
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

    private void VerifySwipe(SwipeData swipeData)
    {
        playerSwipe = swipeData.Direction;
        Debug.Log("Player Swiped: " + playerSwipe);

        // Turn off swipe deterctor
        swipeDetector.enabled = false;

        // If playing stick animation, don't move forward
        if (playingStickAnimation) { return; }

        // TODO: Add feedback when correct/wrong

        if (playerSwipe == currentDirection)
        {
            foldSFX.Play();
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

        // If we've reached the end of the current set, i.e. we're done with one plastic
        if (currentFold >= FoldingSet.MaxCount)
        {
            // Reset current fold count
            currentFold = 0;

            // Move to next fold set
            currentFoldSet++;

            // Update plastic left count
            plasticLeft--;
            UpdatePlasticLeftCountDisplay();

            // Hide the prompt
            Invoke("HidePrompt", lastFoldDisplayDelay * 0.4f);

            // Play feedback; supposedly the bottle's shown being filled up by plastic
            particles.Play();
            yield return new WaitWhile(() => particles.isPlaying);

            Debug.Log("Move to next fold set");

            // Finished an ecobrick
            if ((currentFoldSet != 0) && (currentFoldSet % plasticsPerBottle == 0))
            {
                yield return StartCoroutine(PlayStickAnimation());

                // +1 Ecobrick Count
                ecobrickCount++;
                sessionData.EcobricksDone++;
                UpdateEcobrickCountDisplay();

                // If we've reached the end
                if (currentFoldSet >= prompts.Count - 1)
                {
                    EndGameSession();
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

        playingStickAnimation = true;

        int action = 0;
        while (action != 3)
        {
            // Make sure playerSwipe is clear
            playerSwipe = SwipeDirection.None;

            OnActionPrompt.Invoke();

            // Ask for a tap
            //yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            // Ask for swipeDown
            swipeDetector.enabled = true;
            yield return new WaitUntil(() => PlayerHasSwiped());

            // Turn off swipe detector
            swipeDetector.enabled = false;

            if (playerSwipe == SwipeDirection.Down)
            {
                action++;
                Debug.Log("Action Count: " + action);

                OnActionDone.Invoke();

                // Play stick animation
                stickAnimator.SetBool("PoundStick", true);

                // Wait until we enter current state
                yield return new WaitUntil(() => stickAnimator.GetCurrentAnimatorStateInfo(0).IsName("PoundStick"));

                // Wait for the animation
                yield return new WaitUntil(() => IsDonePlaying(stickAnimator));
                Debug.Log("Done playing stick animation: " + IsDonePlaying(stickAnimator));
                stickSFX.Play();
                stickAnimator.SetBool("PoundStick", false);
            }
        }

        playingStickAnimation = false;
    }

    public void EndGameSession()
    {
        Debug.Log("Minigame End");
        HidePrompt();
        swipeDetector.enabled = false;

        // Turn off animation
        bottleAnimator.SetBool("StartGame", false);
        stickAnimator.SetBool("PoundStick", false);

        // Remove plastics used to create an ecobrick from sessionData list
        int totalPlasticUsed = ecobrickCount * plasticsPerBottle;
        if (totalPlasticUsed > 0)
        {
            //sessionData.SortedPlastic.RemoveRange(0, totalPlasticUsed);
            sessionData.CollectedPlastic.RemoveRange(0, totalPlasticUsed);
        }

        OnGameEnd.Invoke();
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

    private bool PlayerHasSwiped()
    {
        return playerSwipe != SwipeDirection.None;
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

    private void UpdatePlasticLeftCountDisplay()
    {
        plasticLeftCountText.text = plasticLeft.ToString();
    }
}
