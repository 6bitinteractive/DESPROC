using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float PauseDelay;

    public void Pause()
    {       
        Time.timeScale = 0;       
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        StopAllCoroutines();
    }

    public void DelayedPause()
    {
        StartCoroutine(PauseDelayed());
    }

    public IEnumerator PauseDelayed()
    {
        yield return new WaitForSeconds(PauseDelay);
        Pause();
    }
}
