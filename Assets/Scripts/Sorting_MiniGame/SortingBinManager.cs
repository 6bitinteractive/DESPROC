using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingBinManager : MonoBehaviour
{
    public TrashSet bowlSet;
    private SpriteRenderer spriteRenderer;

    private int currentStep = 1;

    private void Start()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    public void SwitchToNextStep()
    {
        spriteRenderer.sprite = bowlSet.steps[currentStep];
        currentStep++;

        if (currentStep >= bowlSet.steps.Length)
            currentStep = 0;
    }
}
