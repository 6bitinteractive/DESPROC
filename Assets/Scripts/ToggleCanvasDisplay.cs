using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCanvasDisplay : MonoBehaviour
{
    CanvasGroup canvasGroup;


    public void Toggle(GameObject gameObject)
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();

        if (canvasGroup != null)
        {
            if (canvasGroup.alpha > 0)
                Hide();
            else
                Show();
        }
    }

    public void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
