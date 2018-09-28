using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public SceneHandler SceneHandler;
    public string NextScene;
    public float CountdownValue;
    public Text CountdownText;

    void Awake()
    {
        CountdownValue += (CountdownValue * GlobalData.Instance.Clock);
    }

    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    void Update()
    {
        CountdownText.text = CountdownValue.ToString(); // Display countdown

        if (CountdownValue <= 0) OnCountdownEnd(); // If countdown ended call onCountdownEnd function

    }

    public IEnumerator StartCountdown()
    {
        while (CountdownValue > 0)
        {
            yield return new WaitForSeconds(1.0f); // Scale timer
            CountdownValue--; // Reduce timer
        }
    }

    void OnCountdownEnd()
    {
      SceneHandler.LoadScene(NextScene);
    }
}
