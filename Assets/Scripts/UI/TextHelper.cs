using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHelper : MonoBehaviour
{
    [SerializeField] private string pcText;
    [SerializeField] private string mobileText;
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();

#if UNITY_STANDALONE
        text.text = pcText;
#endif

#if UNITY_ANDROID || UNITY_IOS
        text.text = mobileText;
#endif
    }
}
