using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text Tortpoints;
    public Text Tortgold;
    public bool isInWorldMap = false;

    void Update()
    {
        if (isInWorldMap)
        {
            DisplayScore();
        }
    }

    public void EarnScore(float scoreValue)
    {
        GlobalData.Instance.Tortpoints += scoreValue;
        GlobalData.Instance.Tortgold += scoreValue;
    }

    public void DisplayScore()
    {
        Tortpoints.text = GlobalData.Instance.Tortpoints.ToString();
        Tortgold.text = GlobalData.Instance.Tortgold.ToString();
    }
}
