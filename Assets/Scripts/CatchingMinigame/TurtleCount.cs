using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurtleCount : MonoBehaviour
{
    [SerializeField] private int count = 5;
    [SerializeField] private Text text;
    public GameEvent OnZero;
    private void Awake()
    {
        text.text = count.ToString();
    }

    public void Decrease()
    {
        Debug.Log("Decreased");
        count--;
        text.text = count.ToString();

        if (count == 0)
            OnZero.Raise();
    }
}
