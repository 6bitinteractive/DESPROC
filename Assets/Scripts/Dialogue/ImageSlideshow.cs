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
    public Button startButton;

    [Header("Dialogue")]
    public DialogueTrigger dialogueTrigger;
    public Text dialogueText;

    private int i = 0;
    private bool readAll;

    void Start()
    {
        readAll = false;
    }

    void Update()
    {
        displayImage.sprite = spriteArray.sprites[mod(i, spriteArray.sprites.Length)];
        dialogueText.text = dialogueTrigger.dialogueArray[0].sentenceArray[mod(i, dialogueTrigger.dialogueArray[0].sentenceArray.Length)].sentence.ToString();
    }

    public void BtnNext()
    {
        if (i >= spriteArray.sprites.Length - 1)
        {
            i = spriteArray.sprites.Length;
            readAll = true;
            nextImage.gameObject.SetActive(false);

            if(readAll)
            {
                startButton.gameObject.SetActive(true);
            }
        }
        else
        {
            i++;
            previousImage.gameObject.SetActive(true);
        }
    }

    public void BtnPrev()
    {
        if (i <= 0)
        {
            i = 0;
            previousImage.gameObject.SetActive(false);
        }
        else
        {
            i--;
            nextImage.gameObject.SetActive(true);
        }
    }

    private int mod(int x, int m)
    {
        // Custom modulo function (C# modulo operation does not operate as intended on negative numbers)
        return (x % m + m) % m;
    }
}
