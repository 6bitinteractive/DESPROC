using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSlideshow : MonoBehaviour
{
    [Header("Display")]
    public SpriteArray spriteArray;
    public Image displayImage;
    public Button nextImage;
    public Button previousImage;

    [Header("Dialogue")]
    public DialogueTrigger dialogueTrigger;
    public Text dialogueText;

    private int i = 0;

    void Update()
    {
        displayImage.sprite = spriteArray.sprites[mod(i, spriteArray.sprites.Length)];
        dialogueText.text = dialogueTrigger.dialogueArray[0].sentenceArray[mod(i, dialogueTrigger.dialogueArray[0].sentenceArray.Length)].sentence.ToString();
    }

    public void BtnNext()
    {
        i++;
    }

    public void BtnPrev()
    {
         i--;
    }

    private int mod(int x, int m)
    {
        // Custom modulo function (C# modulo operation does not operate as intended on negative numbers)
        return (x % m + m) % m;
    }
}
