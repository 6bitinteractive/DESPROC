using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]

public class TurtleCount : MonoBehaviour
{
    [SerializeField] private int count = 5;
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
        text.text = count.ToString();
    }

    public void Decrease()
    {
        count--;
        text.text = count.ToString();
    }
}
