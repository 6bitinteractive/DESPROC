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
    public DialogueTrigger dialogue;
    public Text dialogueText;

    private int i = 0;

    void Update()
    {
        displayImage.sprite = spriteArray[i % spriteArray.Length];
        dialogueText.text = dialogue.dialogueArray[0].sentenceArray[i % dialogue.dialogueArray[0].sentenceArray.Length].sentence.ToString();
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
