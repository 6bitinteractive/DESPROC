using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayEvent : MonoBehaviour {

    public float SecondsBeforeRunning = 0;

    public UnityEngine.Events.UnityEvent EventsToRun;

    public void Run()
    {
        StartCoroutine(delayRun());
    }
    IEnumerator delayRun()
    {
        yield return new WaitForSeconds(SecondsBeforeRunning);
        EventsToRun.Invoke();
    }
}
