using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarSlice : MonoBehaviour
{
    [HideInInspector] public Image Mark;

    private void Start()
    {
        Mark = GetComponent<Image>();
    }

    public void SetImageMark(Sprite sprite)
    {
        Mark.sprite = sprite;
    }
}
