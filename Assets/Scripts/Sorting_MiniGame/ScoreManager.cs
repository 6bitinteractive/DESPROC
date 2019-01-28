using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static float Score;
    public Text ScoreText;

    void Update()
    {
        DisplayScore();
    }

    public static void EarnScore(float scoreValue)
    {
        Score += scoreValue;
    }

    public void DisplayScore()
    {
        ScoreText.text = Score.ToString();
    }
}