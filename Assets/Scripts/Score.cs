using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text TortpointsText;
    public Text TortgoldText;
    public bool ShowScore = false;

    void Update()
    {
        if (ShowScore)
        {
            DisplayScore();
        }
    }

    public void EarnScore(float scoreValue)
    {
        GlobalData.Instance.Tortpoints += scoreValue + (scoreValue * GlobalData.Instance.Luck);
        GlobalData.Instance.Tortgold += scoreValue + (scoreValue * GlobalData.Instance.Luck);
    }

    public void DisplayScore()
    {
        TortpointsText.text = GlobalData.Instance.Tortpoints.ToString();
        TortgoldText.text = GlobalData.Instance.Tortgold.ToString();
    }
}
