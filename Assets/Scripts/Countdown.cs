using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public GameEvent CountdownEndScreen;
    public float CountdownValue;
    public Text CountdownText;

    [SerializeField] private GameDataHandler gameDataHandler;

    void Awake()
    {
        if (gameDataHandler == null)
        {
            Debug.LogWarning("GameDataHandler object is null; set through FindByObjectOfType<>.");
            gameDataHandler = FindObjectOfType<GameDataHandler>();
        }

        // Fix: loading of data occurs on Start (in the GameDataHandler script) so this occurs first and doesn't use the loaded value from the JSON file
        // This simply uses whatever is set in the inspector
        GameData gameData = gameDataHandler.gameData;
        CountdownValue += (CountdownValue * gameData.Clock);
    }

    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    void Update()
    {
        CountdownText.text = Mathf.Floor(CountdownValue / 60).ToString("00") + ":" + Mathf.FloorToInt(CountdownValue % 60).ToString("00"); // Display countdown

        // NOTE: For UX, turn text to red when it's less than 10s left
        if(CountdownValue > 0 && CountdownValue <= 10f)
        {
            CountdownText.color = Color.red;
            return;
        }

        if (CountdownValue <= 0)
        {
           OnCountdownEnd(); // If countdown ended call onCountdownEnd function
        }
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
        CountdownEndScreen.Raise();
    }
}
