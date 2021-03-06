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
    [SerializeField] private GameObject plasticPrefab;
    [SerializeField] private Transform plasticPosition;
    [SerializeField] private Sorting.SortingBin[] sortingBins;
    [SerializeField] private List<PlasticData> plasticDatas;

    [Header("UI Display")]
    [SerializeField] private Text timeText;
    [SerializeField] private Text bestTimeText;
    [SerializeField] private Text plasticLeftCountText;
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
        for (int i = 0; i < sessionData.CollectedPlastic.Count; i++)
        {
            GameObject plastic = Instantiate(plasticPrefab, plasticPosition.position, Quaternion.identity);
            plastic.GetComponent<Plastic>().PlasticData = plasticDatas[Random.Range(0, plasticDatas.Count)];
            plastic.transform.SetParent(plasticPosition);
            plastic.SetActive(false);

            plasticsToSort.Add(plastic);
        }

        UpdatePlasticLeftCountDisplay();

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
        UpdatePlasticLeftCountDisplay();

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

        // Show time/plastic
        float timePerPlastic = timer / (float)sessionData.CollectedPlastic.Count;
        timeText.text = string.Format("{0:00.00}/plastic", timePerPlastic);

        // Check if its a new record for best time
        if (timePerPlastic < sessionData.SortingBestTime || sessionData.SortingBestTime <= 0f)
        {
            Debug.Log("Set a new record.");
            sessionData.SortingBestTime = timePerPlastic;
            bestTimeText.text = string.Format("BEST {0:00.00}", sessionData.SortingBestTime);
        }

        // Add sorted plastic to sessionData's ecobrick list
        foreach (var plastic in sortedPlastics)
            sessionData.SortedPlastic.Add(plastic.GetComponent<Plastic>().PlasticData);

        // Add back whatever's unsorted to the collected list
        sessionData.CollectedPlastic.Clear();
        foreach (var plastic in plasticsToSort)
            sessionData.CollectedPlastic.Add(plastic.GetComponent<Plastic>().PlasticData);

        OnEnd.Invoke();
    }

    private void UpdatePlasticLeftCountDisplay()
    {
        plasticLeftCountText.text = plasticsToSort.Count.ToString();
    }
}
