using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image ProgressBarFill;
    public Text ProgressBarText;

    private int total;
    private int saved = 0;

    private void Start()
    {
        total = TurtleManager.TotalTurtles;
        UpdateBar();
    }

    public void UpdateProgress()
    {
        saved++;
        UpdateBar();
    }

    private void UpdateBar()
    {
        float percentage = 0f;

        if (total != 0)
            percentage = saved / total;

        ProgressBarFill.fillAmount = percentage;
        ProgressBarText.text = string.Format("Turtles Saved: {0} %", Mathf.RoundToInt(percentage * 100f));
    }
}
