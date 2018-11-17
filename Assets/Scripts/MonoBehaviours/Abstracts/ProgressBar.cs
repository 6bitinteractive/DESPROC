using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class ProgressBar : MonoBehaviour
{
    [SerializeField] protected Image ProgressBarFill;
    [SerializeField] protected Text ProgressBarText;

    protected int total;
    protected int current = 0;

    protected virtual void Start()
    {
        UpdateBar();
    }

    public virtual void UpdateProgress()
    {
        current++;
        UpdateBar();
    }

    protected virtual void UpdateBar()
    {
        float percentage = 0f;

        if (total > 0)
            percentage = (float)current / (float)total; // Needs to be float or else it's always 0%

        ProgressBarFill.fillAmount = percentage;
        SetProgressBarText(percentage);
    }

    protected abstract void SetProgressBarText(float percentage);
}
