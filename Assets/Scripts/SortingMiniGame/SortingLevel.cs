using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SortingLevel : MonoBehaviour
{
    //[Header("Testing")]
    //[Tooltip("For testing only")]
    //[SerializeField] List<GameObject> plasticPrefabs = new List<GameObject>();

    [Header("Setup")]
    [SerializeField] private TurtleTale.SessionData sessionData;
    [SerializeField] private GameObject plasticPrefab;
    [SerializeField] Transform plasticPosition;
    [SerializeField] Sorting.SortingBin[] sortingBins;

    [Header("UI Display")]
    [SerializeField] private Text timeText;
    [SerializeField] private Text bestTimeText;
    [SerializeField] private GameObject actionFeedbackPanel;

    public UnityEvent OnCorrectSort = new UnityEvent();
    public UnityEvent OnIncorrectSort = new UnityEvent();
    public UnityEvent OnEnd = new UnityEvent();

    private List<GameObject> plasticsToSort = new List<GameObject>();
    private List<GameObject> sortedPlastics = new List<GameObject>();
    private float timer;
    private bool gameEnd;

    private void Awake()
    {
        if (sessionData == null)
            Debug.LogError("No session data.");

        if (sortingBins.Length <= 0)
            Debug.LogError("No sorting bins.");

        if (plasticPosition == null)
            Debug.LogError("Plastic position is not set.");
    }

    private void OnEnable()
    {
        foreach (var bin in sortingBins)
        {
            bin.OnVerify.AddListener(Sort);
        }
    }

    private void OnDisable()
    {
        foreach (var bin in sortingBins)
        {
            bin.OnVerify.RemoveListener(Sort);
        }
    }

    private void Start()
    {
        // Display best time
        if (sessionData.SortingBestTime > 0f)
        {
            bestTimeText.text = string.Format("BEST {0:00.00}", sessionData.SortingBestTime);
        }

        // Get plastics from sessionData
        plasticsToSort.AddRange(sessionData.CollectedPlastic);

        // Enable DroppableToBin component; position plastics; hide
        foreach (var plastic in plasticsToSort)
        {
            plastic.GetComponent<DroppableToBin>().enabled = true;
            plastic.transform.position = plasticPosition.position;
            plastic.transform.SetParent(plasticPosition);
            plastic.SetActive(false);
        }

        ShowPlastic();
    }

    private void Update()
    {
        if (gameEnd) { return; }

        // Timer
        timer += Time.deltaTime;
        timeText.text = string.Format("{0:00.00}", timer);
    }

    private void ShowPlastic()
    {
        if (plasticsToSort.Count > 0)
        {
            plasticsToSort[0].SetActive(true);
        }
        else
        {
            EndLevel();
        }
    }

    private void Sort(GameObject obj, RecycleCode binRecycleCode)
    {
        RecycleCode plasticRecycleCode = obj.GetComponent<Plastic>().PlasticData.RecycleCode;
        bool correctlySorted = plasticRecycleCode == binRecycleCode;

        if (plasticsToSort[0] == obj)
        {
            // Hide the object
            obj.SetActive(false);
        }

        // Enable feedback panel
        actionFeedbackPanel.SetActive(true);

        if (correctlySorted)
        {
            Debug.Log("Correctly sorted");

            // Add to sortedPlastics list
            sortedPlastics.Add(obj);

            // Remove from plasticsToSort list
            plasticsToSort.RemoveAt(0);

            OnCorrectSort.Invoke();
        }
        else
        {
            Debug.Log("Sort again.");

            // Reset position to plasticPosition
            obj.transform.position = plasticPosition.position;

            OnIncorrectSort.Invoke();
        }

        // Show next plastic to sort
        ShowPlastic();
    }

    private void EndLevel()
    {
        // If player quits or reaches end of list
        Debug.Log("End of Sorting minigame.");

        gameEnd = true;

        // Check if its a new record for best time
        if (timer < sessionData.SortingBestTime || sessionData.SortingBestTime <= 0f)
        {
            Debug.Log("Set a new record.");
            sessionData.SortingBestTime = timer;
            bestTimeText.text = string.Format("BEST {0:00.00} - New Record", sessionData.SortingBestTime);
        }

        // Disable DroppableToBin component
        foreach (var plastic in sortedPlastics)
            plastic.GetComponent<DroppableToBin>().enabled = false;

        foreach (var plastic in plasticsToSort)
            plastic.GetComponent<DroppableToBin>().enabled = false;

        // Add sorted plastic to sessionData's ecobrick list
        sessionData.SortedPlastic.AddRange(sortedPlastics);

        // Add back whatever's unsorted to the collected list
        sessionData.CollectedPlastic.Clear();
        sessionData.CollectedPlastic.AddRange(plasticsToSort);

        OnEnd.Invoke();
    }
}
