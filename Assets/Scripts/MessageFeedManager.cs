using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageFeedManager : MonoBehaviour
{

    [SerializeField] private GameObject messagePrefab;

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

    public void WriteMessage(string message)
    {
        GameObject gameObject = Instantiate(messagePrefab, transform);
        gameObject.GetComponent<Text>().text = message;
        gameObject.transform.SetAsFirstSibling();// Moves text upwards
        Destroy(gameObject, 2); // Destroy after 2 seconds
    }
}
