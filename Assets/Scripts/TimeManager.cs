using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public void Pause()
    {       
        Time.timeScale = 0;       
    }

    public void Unpause()
    {
        Time.timeScale = 1;
    }
}
