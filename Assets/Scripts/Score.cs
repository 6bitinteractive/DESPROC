using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerDataHandler))]

public class Score : MonoBehaviour
{
    public Text TortpointsText;
    public Text TortgoldText;
    public bool ShowScore = false;

    PlayerData playerData;

    private void Start()
    {
        playerData = GetComponent<PlayerDataHandler>().PlayerData;
    }

    void Update()
    {
        if (ShowScore)
        {
            DisplayScore();
        }
    }

    public void EarnScore(float scoreValue)
    {
        playerData.Tortpoints += scoreValue + (scoreValue * playerData.Luck);
        playerData.Tortgold += scoreValue + (scoreValue * playerData.Luck);
    }

    public void DisplayScore()
    {
        TortpointsText.text = playerData.Tortpoints.ToString();
        TortgoldText.text = playerData.Tortgold.ToString();
    }
}
