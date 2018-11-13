using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurtlesSavedProgressBar : ProgressBar
{
    public UnityEvent OnSavedEnoughTurtles = new UnityEvent();

    protected override void Start()
    {
        total = TurtleManager.TotalTurtles;

        base.Start();
    }

    protected override void UpdateBar()
    {
        base.UpdateBar();

        if (current >= total * GameState.MinTurtlesToSavePercentage)
            OnSavedEnoughTurtles.Invoke();
    }

    protected override void SetProgressBarText(float percentage)
    {
        ProgressBarText.text = string.Format("Turtles Saved: {0} %", Mathf.RoundToInt(percentage * 100f));
    }
}
