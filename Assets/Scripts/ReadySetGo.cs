﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadySetGo : MonoBehaviour
{
    public float ChangeSpriteTimer;
    public Sprite[] SpritesArray;

    private void Start()
    {
        StartCoroutine(readySetGoTimer(ChangeSpriteTimer));

        // Ready
        gameObject.GetComponent<SpriteRenderer>().sprite = SpritesArray[0];
    }

    private IEnumerator readySetGoTimer(float changeSpriteTimer)
    {
        // Set
        yield return new WaitForSecondsRealtime(4); // Wait for seconds real time is not affected by time.timescale
        gameObject.GetComponent<SpriteRenderer>().sprite = SpritesArray[1];

        // Go
        yield return new WaitForSecondsRealtime(2); 
        gameObject.GetComponent<SpriteRenderer>().sprite = SpritesArray[2];

        yield return new WaitForSecondsRealtime(0.5f); 
        gameObject.SetActive(false);
    }
}
