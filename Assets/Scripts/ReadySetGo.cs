using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadySetGo : MonoBehaviour
{
    public float ChangeSpriteTimer;
    public Sprite[] SpritesArray;
    public AudioSource CountdownSFX;

    private PlayerCatchingMiniGameController controller;

    private void Start()
    {
        controller = FindObjectOfType<PlayerCatchingMiniGameController>();
        // Stop player from moving
        controller.enabled = false;

        StartCoroutine(readySetGoTimer(ChangeSpriteTimer));

        // Ready
        gameObject.GetComponent<SpriteRenderer>().sprite = SpritesArray[0];
    }

    private IEnumerator readySetGoTimer(float changeSpriteTimer)
    {
        // Set
        yield return new WaitForSecondsRealtime(4); // Wait for seconds real time is not affected by time.timescale
        gameObject.GetComponent<SpriteRenderer>().sprite = SpritesArray[1];

        // Go
        yield return new WaitForSecondsRealtime(2);
        CountdownSFX.Play();
        gameObject.GetComponent<SpriteRenderer>().sprite = SpritesArray[2];

        yield return new WaitForSecondsRealtime(0.5f);
        gameObject.SetActive(false);

        // Player can move again
        controller.enabled = true;
    }
}
