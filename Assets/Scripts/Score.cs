using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerDataHandler))]

public class Score : MonoBehaviour
{
    [SerializeField] private TurtleTale.SessionData sessionData;
    public Text TortpointsText;
    public Text TortgoldText;
    public bool ShowScore = false;

    //PlayerData playerData;

    //private void Start()
    //{
    //    playerData = GetComponent<PlayerDataHandler>().playerData;
    //}

    void Update()
    {
        if (ShowScore)
        {
            DisplayScore();
        }
    }

    public void EarnScore(float scoreValue)
    {
        //playerData.Tortpoints += scoreValue + (scoreValue * playerData.Luck);
        //playerData.Tortgold += scoreValue + (scoreValue * playerData.Luck);
        sessionData.Tortpoints += scoreValue + (scoreValue * sessionData.Luck);
        sessionData.Tortgold += scoreValue + (scoreValue * sessionData.Luck);
    }

    public void DisplayScore()
    {
        //TortpointsText.text = playerData.Tortpoints.ToString();
        //TortgoldText.text = playerData.Tortgold.ToString();
        TortpointsText.text = sessionData.Tortpoints.ToString();
        TortgoldText.text = sessionData.Tortgold.ToString();
    }
}
