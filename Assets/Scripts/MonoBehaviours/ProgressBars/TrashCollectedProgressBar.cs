using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrashCollectedProgressBar : ProgressBar
{
    public UnityEvent OnCollectedEnoughTrash = new UnityEvent();

    protected override void UpdateBar()
    {
        base.UpdateBar();

        if (current >= total * GameState.MinTrashToCollect)
            OnCollectedEnoughTrash.Invoke();
    }

    protected override void SetProgressBarText(float percentage)
    {
        ProgressBarText.text = string.Format("Trash Collected: {0} %", Mathf.RoundToInt(percentage * 100f));
    }

    protected override void InitializeData()
    {
        total = GameState.MinTrashToCollect;
    }
}
