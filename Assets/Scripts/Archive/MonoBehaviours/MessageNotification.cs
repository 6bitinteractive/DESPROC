using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageNotification : MonoBehaviour
{
    [SerializeField] private GameObject messagePrefab;
    public float MessageDuration = 1.5f;

    private GameObject messageObj;
    private Text messageText;
    private RectTransform messageObjRectTransform;
    private RectTransform parentRectTransform;

    private void Start()
    {
        messageObj = Instantiate(messagePrefab);
        messageText = messageObj.GetComponentInChildren<Text>();
        messageObjRectTransform = messageObj.GetComponent<RectTransform>();
        parentRectTransform = GetComponent<RectTransform>();
    }

    public void ShowMessage(string message)
    {
        messageText.text = message;
        messageObjRectTransform.anchoredPosition = parentRectTransform.anchoredPosition;
        messageObj.transform.SetParent(transform, false);
        messageObj.SetActive(true);

        Invoke("HideMessage", MessageDuration);
    }

    private void HideMessage()
    {
        messageObj.SetActive(false);
    }
}
