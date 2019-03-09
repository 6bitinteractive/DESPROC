﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLevel : MonoBehaviour
{
    [Tooltip("For testing only")]
    [SerializeField] GameObject plasticPrefab;

    [SerializeField] private TurtleTale.SessionData sessionData;
    [SerializeField] Transform plasticPosition;
    [SerializeField] Sorting.SortingBin[] sortingBins;

    private List<GameObject> plasticsToSort = new List<GameObject>();
    private List<GameObject> sortedPlastics = new List<GameObject>();

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
        // TODO: Get plastics from the inventory(?)
        //plasticsToSort.AddRange(sessionData.PickedUpList);

        // For testing
        for (int i = 0; i < 5; i++)
        {
            plasticsToSort.Add(Instantiate(plasticPrefab, plasticPosition.position, Quaternion.identity));
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
        RecycleCode plasticRecycleCode = obj.GetComponent<PlasticInteractable>().GetPlasticData().RecycleCode;
        bool correctlySorted = plasticRecycleCode == binRecycleCode;

        if (plasticsToSort[0] == obj)
        {
            // Hide the object
            obj.SetActive(false);

            // Remove from plasticsToSort list
            plasticsToSort.RemoveAt(0);
        }

        // TODO: Add feedback

        if (correctlySorted)
        {
            Debug.Log("Correctly sorted");

            // Add to sortedPlastics list
            sortedPlastics.Add(obj);
        }
        else
        {
            Debug.Log("Sort again.");

            // Reset position to plasticPosition
            obj.transform.position = plasticPosition.position;

            // Add back to end of plasticToSort list
            plasticsToSort.Add(obj);
        }

        // Show next plastic to sort
        ShowPlastic();
    }

    private void EndLevel()
    {
        // If player quits or reaches end of list
        Debug.Log("End of Sorting minigame.");

        // Add sorted plastic to sessionData's ecobrick list
        sessionData.ForEcobrick.AddRange(sortedPlastics);

        // Add back whatever's unsorted to the pickedup list
        sessionData.PickedUpPlastic.Clear();
        sessionData.PickedUpPlastic.AddRange(plasticsToSort);
    }
}
