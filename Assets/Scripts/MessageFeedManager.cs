using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageFeedManager : MonoBehaviour
{
    [SerializeField] private GameObject messagePrefab;
    private Image background;

    private static MessageFeedManager instance;

    public static MessageFeedManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MessageFeedManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        background = GetComponent<Image>();
    }

    public void WriteMessage(string message)
    {
        background.enabled = true; // Show background
        GameObject gameObject = Instantiate(messagePrefab, transform);
        gameObject.GetComponent<Text>().text = message;
        gameObject.transform.SetAsFirstSibling();// Moves text upwards
        Destroy(gameObject, 2.3f); // Destroy after a few seconds
        Invoke("HideBackground", 2.3f); // Hide background
    }

    private void HideBackground()
    {
        background.enabled = false;
    }
}
