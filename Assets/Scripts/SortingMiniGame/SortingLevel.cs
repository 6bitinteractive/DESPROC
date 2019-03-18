﻿using System;
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

        // Get plastics from the inventory
        plasticsToSort.AddRange(sessionData.PickedUpPlastic);

        for (int i = 0; i < 5; i++)
        {
            // For testing
            //int randomIndex = Random.Range(0, plasticPrefabs.Count);
            //plasticsToSort.Add(Instantiate(plasticPrefabs[randomIndex], plasticPosition.position, Quaternion.identity));

            plasticsToSort[i].transform.position = plasticPosition.position;

            if (plasticsToSort[i].GetComponent<DroppableToBin>() == null)
                plasticsToSort[i].AddComponent<DroppableToBin>();
        }

        // Hide
        foreach (var plastic in plasticsToSort)
        {
            plastic.SetActive(false);
            plastic.transform.SetParent(plasticPosition);
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
        RecycleCode plasticRecycleCode = obj.GetComponent<PlasticInteractable>().PlasticData.RecycleCode;
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

        // Add sorted plastic to sessionData's ecobrick list
        sessionData.ForEcobrick.AddRange(sortedPlastics);

        // Add back whatever's unsorted to the pickedup list
        sessionData.PickedUpPlastic.Clear();
        sessionData.PickedUpPlastic.AddRange(plasticsToSort);

        OnEnd.Invoke();
    }
}
