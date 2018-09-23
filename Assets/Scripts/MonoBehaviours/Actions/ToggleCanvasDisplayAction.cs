using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]

public class ToggleCanvasDisplayAction : Action
{
    CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }

    public override void Act()
    {
        Toggle();
    }

    private void Toggle()
    {
        if (canvasGroup != null)
        {
            if (canvasGroup.alpha > 0)
                Hide();
            else
                Show();
        }
    }

    private void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
    }

    private void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
    }
}
