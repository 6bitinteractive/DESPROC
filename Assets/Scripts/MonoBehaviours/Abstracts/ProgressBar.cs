using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class ProgressBar : MonoBehaviour
{
    [SerializeField] protected TurtleTale.SessionData sessionData;
    //[SerializeField] protected PlayerDataHandler playerDataHandler;
    [SerializeField] protected Image ProgressBarFill;
    [SerializeField] protected Text ProgressBarText;

    protected int total;
    protected int current;

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
        InitializeData();
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
    protected abstract void InitializeData();
}
