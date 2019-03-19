using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSlideshow : MonoBehaviour
{
    [Header("Display")]
    public Sprite[] spriteArray;
    public Image displayImage;
    public Button nextImage;
    public Button previousImage;

    [Header("Dialogue")]
    public DialogueTrigger dialogueTrigger;
    public Text dialogueText;

    private int i = 0;

    void Update()
    {
        displayImage.sprite = spriteArray[i % spriteArray.Length];
        dialogueText.text = dialogueTrigger.dialogueArray[0].sentenceArray[i % dialogueTrigger.dialogueArray[0].sentenceArray.Length].sentence.ToString();
    }

    public void BtnNext()
    {
        i++;
    }

    public void BtnPrev()
    {
         i--;
    }
}
